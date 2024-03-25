using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace worker_socket.utils
{
    public class Jwt
    {
        private readonly ILogger<Jwt> logger;

        public Jwt(
            ILogger<Jwt> logger
        )
        {
            this.logger = logger;
        }
        public string CreateToken()
        {
            var publicKeyPem = Environment.GetEnvironmentVariable("RSA");
            this.logger.LogInformation("Get path publicKeyPem", publicKeyPem);
            var publicKey = RSA.Create();
            
            publicKey.ImportFromPem(publicKeyPem);//erro con el certificado corregir
            
            var payload = new Dictionary<string, object>
                {
                    { "claim1", 0 },
                    { "claim2", "claim2-value" }
                };

            IJwtAlgorithm algorithm = new RS256Algorithm(publicKey);
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            const string key = null; // not needed if algorithm is asymmetric

            var token = encoder.Encode(payload, key);
            Console.WriteLine(token);
            return token;
        }
    }
}