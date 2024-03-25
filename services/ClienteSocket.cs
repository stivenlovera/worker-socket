using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Websocket.Client;
using worker_socket.emisores;
using worker_socket.models;

namespace worker_socket.services
{
    public class ClienteSocket : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;

        public ClienteSocket(
            ILogger<Worker> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration
        )
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new Func<ClientWebSocket>(() =>
            {
                var client = new ClientWebSocket
                {
                    Options =
                    {
                        KeepAliveInterval = TimeSpan.FromSeconds(5),
                    }
                };
                client.Options.SetRequestHeader("token", "worker");
                client.Options.SetRequestHeader("origin", "localhost");
                return client;
            });

            var exitEvent = new ManualResetEvent(false);
            var host = this.configuration.GetValue<string>("ws");
            var url = new Uri(host);

            using (var client = new WebsocketClient(url, factory))
            {
                client.ReconnectTimeout = TimeSpan.FromDays(7);
                client.ReconnectionHappened.Subscribe(info => this.logger.LogInformation("Conexion establesida: {info}", info.Type.ToString()));

                client.MessageReceived.Subscribe(async (msg) =>
                {
                    using (var scope = this.serviceProvider.CreateAsyncScope())
                    {
                        var verificar = JsonConvert.DeserializeObject<MessageSocket<object>>(msg.ToString());
                        switch (verificar.Event)
                        {
                            case "devices":
                                await scope.ServiceProvider.GetRequiredService<GeneralController>().ApiRequest(msg.ToString(), client);
                                break;
                            //general
                            default:
                                await scope.ServiceProvider.GetRequiredService<GeneralController>().ApiRequest(msg.ToString(), client);
                                break;
                        }
                    }
                    //await client.Reconnect();
                    //client.Dispose();
                });

                await client.Start();

                exitEvent.WaitOne();
                await Task.Run(() =>
                {
                    this.logger.LogInformation("Inciando cliente conexion socket {host}", host);
                });
            }
        }
    }
}