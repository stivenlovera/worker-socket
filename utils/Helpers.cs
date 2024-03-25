using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace worker_socket.utils
{
    public class Helpers
    {
        public static string Log(object data)
        {
            var resultado = JsonConvert.SerializeObject(data, Formatting.Indented);
            return resultado;
        }
        public static string FirstMayus(string texto)
        {
            return Regex.Replace(texto.ToLower(), @"((^\w)|(\s|\p{P})\w)", match => match.Value.ToUpper());
        }
    }
}