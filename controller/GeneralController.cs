using System.Net.WebSockets;
using Newtonsoft.Json;
using Websocket.Client;
using worker_socket.dtos.socket;
using worker_socket.models;
using worker_socket.utils;

namespace worker_socket.emisores
{
    public class GeneralController
    {
        private readonly ILogger<GeneralController> logger;
        private readonly ApiRest apiRest;

        public GeneralController(
            ILogger<GeneralController> logger,
            ApiRest apiRest
        )
        {
            this.logger = logger;
            this.apiRest = apiRest;
        }
        public async Task ApiRequest(string request, WebsocketClient client)
        {
            var message = JsonConvert.DeserializeObject<MessageSocket<List<LectorData<object>>>>(request.ToString());
            this.logger.LogInformation("GeneralController/ApiRequest({request})", Helpers.Log(message));
            var resultados = new List<object>();

            await Parallel.ForEachAsync(message.Data, async (lector, ct) =>
            {
                var apiRequest = await this.apiRest.RequestApi(lector.userLector, lector.passLector, lector.ipLector, lector.endpoint, lector.method, lector.data);

                apiRequest.Request = lector;
                resultados.Add(apiRequest);
            });

            var type = message.Type.Split(":");
            var resultado = new MessageSocket<List<object>>()
            {
                Channel = message.Channel,
                Event = message.Event,
                Data = resultados,
                Type = $"{type[0]}:finalize",
                Adjuntos = message.Adjuntos
            };
            this.logger.LogInformation("GeneralController/ApiRequest enviando resultado => {resultado}", Helpers.Log(resultado));
            client.Send(Newtonsoft.Json.JsonConvert.SerializeObject(resultado));
        }
    }
}