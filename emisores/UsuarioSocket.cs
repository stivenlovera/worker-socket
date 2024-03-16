using System.Net.WebSockets;
using Websocket.Client;
using worker_socket.models;

namespace worker_socket.emisores
{
    public class UsuarioSocket
    {
        private readonly ILogger<UsuarioSocket> logger;

        public UsuarioSocket(
            ILogger<UsuarioSocket> logger
        )
        {
            this.logger = logger;
        }
        public string Escuchando()
        {
            var factory = new Func<ClientWebSocket>(() => new ClientWebSocket
            {
                Options =
                    { },
                HttpResponseHeaders = { }
            });

            var exitEvent = new ManualResetEvent(false);
            var url = new Uri("ws://localhost:8000");

            using (var client = new WebsocketClient(url, factory))
            {

                var message = new MessageSocket<string>()
                {
                    Data = "cliente añadido",
                    Channel = "worker",
                    Event = "usuario",
                    Type = "store:finalize"
                };

                client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                client.ReconnectionHappened.Subscribe(info =>
                this.logger.LogInformation("Reconnection happened, type: {info}", info.Type.ToString()));

                client.MessageReceived.Subscribe(async (msg) =>
                {
                    this.logger.LogInformation("te escuche: {msg}", msg);
                    client.Send(Newtonsoft.Json.JsonConvert.SerializeObject(message));
                    this.logger.LogInformation("te response : {msg}", message);
                    //await client.Reconnect();
                    //client.Dispose();
                });

                client.Start();

                Task.Run(() =>
                {
                    this.logger.LogInformation("enviando mensaje,");
                });
                exitEvent.WaitOne();
            }
            return "";
        }
    }
}