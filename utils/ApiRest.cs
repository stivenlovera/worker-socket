using RestSharp;
using RestSharp.Authenticators.Digest;
using Serilog;

namespace worker_socket.utils
{
    public class ApiRest
    {
        private readonly ILogger<ApiRest> logger;

        public ApiRest(
            ILogger<ApiRest> logger
        )
        {
            this.logger = logger;
        }
        public async Task<ResponseRequest> RequestApi(string usuario, string password, string url, string endpoint, string method, object data)
        {
            this.logger.LogInformation("ApiRest/RequestApi({url},{endpoint},{method},{data})", url, endpoint, method, Helpers.Log(data));
            var resultado = new ResponseRequest();
            try
            {
                var metodo = this.Metodo(method);
                var restOptions = new RestClientOptions($"http://{url}")
                {
                    Authenticator = new DigestAuthenticator(usuario, password)
                };

                var client = new RestClient(restOptions);
                var request = new RestRequest(endpoint, metodo);

                request.AddBody(Newtonsoft.Json.JsonConvert.SerializeObject(data), "application/json");

                var response = await client.ExecuteAsync(request);
                //Console.ReadKey(true);
                resultado.StatusDescription = response.StatusDescription.ToString();
                resultado.StatusCode = (int)response.StatusCode;
                resultado.Response = response.Content;
                this.logger.LogInformation("SUCCESS => {response}", Helpers.Log(resultado));
            }
            catch (System.Exception ex)
            {
                resultado.StatusDescription = ex.Message;
                resultado.StatusCode = 500;
                resultado.Response = new { };
                this.logger.LogCritical("ERROR => {response}", Helpers.Log(resultado));
            }

            return resultado;
        }
        public RestSharp.Method Metodo(string Metodo)
        {
            switch (Metodo)
            {
                case "POST":
                    return RestSharp.Method.Post;
                case "GET":
                    return RestSharp.Method.Get;
                case "PUT":
                    return RestSharp.Method.Put;
                case "DELETE":
                    return RestSharp.Method.Delete;
                default:
                    return RestSharp.Method.Get;
            }
        }
    }
    public class ResponseRequest
    {
        public string StatusDescription { get; set; }
        public int StatusCode { get; set; }
        public object Response { get; set; }
        public object Request { get; set; }
    }
}