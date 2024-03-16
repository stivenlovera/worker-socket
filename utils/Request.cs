using RestSharp;
using RestSharp.Authenticators.Digest;
using System.Text.Json;
using worker_socket.models;

namespace worker_socket.utils
{
    public class Request
    {
        public async Task<string> RequestFormData()
        {
            var restOptions = new RestClientOptions("http://192.168.1.246") ///http://localhost:3300  http://192.168.1.246
            {
                Authenticator = new DigestAuthenticator("admin", "molomix654")
            };

            var client = new RestClient(restOptions);
            var request = new RestRequest("/ISAPI/Intelligent/FDLib/FDSetUp?format=json", Method.Put); ///anime/portada ///ISAPI/Intelligent/FDLib/FDSetUp?format=json
            //request.AddHeader("Content-Type", "multipart/form-data");
            //request.AddHeader("Content-Type","application/x-www-form-urlencoded");
            var fileBytes = File.ReadAllBytes("nueva_imagen.jpeg");
            String file = Convert.ToBase64String(fileBytes);
            System.Console.WriteLine(fileBytes.Length);

            var FaceDataRecord = new
            {
                faceLibType = "blackFD",
                FDID = "1",
                FPID = "15",
                faceURL = "https://gym-admin.todo-soft.net/imagenes/clientes/image65ec314fd83471709977935.jpg"
            };
            var foto = new
            {
                faceLibType = "blackFD",
                FDID = "1",
                FPID = "15"
            };

            //request.AddFile("FaceDataRecord",JsonSerializer.Serialize(foto));
            //request.AlwaysMultipartFormData = true;
            //request.AddParameter(new BodyParameter("FaceDataRecord", JsonSerializer.Serialize(FaceDataRecord), "application/json"));
            //request.AddParameter("FaceDataRecord", JsonSerializer.Serialize(FaceDataRecord));
            //request.AddParameter("application/x-www-form-urlencoded", $"faceLibType=blackFD&FDID=1&FPID=15", ParameterType.RequestBody);
            //request.AddFile("faceURL", @"c:\nueva_imagen.jpeg");
            //request.AddFile("FaceDataRecord", @"c:\data.json");
            request.AddBody(FaceDataRecord, "application/json");
            //request.AddParameter("FaceDataRecord", Newtonsoft.Json.JsonConvert.SerializeObject(FaceDataRecord), ParameterType.RequestBody);
            //request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(FaceDataRecord), ParameterType.RequestBody);
            //request.AddBody(FaceDataRecord,"application/json");
            //request.AddUrlSegment("FaceDataRecord", Newtonsoft.Json.JsonConvert.SerializeObject(FaceDataRecord));




            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);
            //Console.ReadKey(true);
            return "";
        }
    }
}