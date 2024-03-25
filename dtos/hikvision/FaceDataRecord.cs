using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace worker_socket.dtos.hikvision
{
    public class FaceDataRecord
    {
        public string faceLibType { get; set; }
        public string FDID { get; set; }
        public string FPID { get; set; }
        public string faceURL { get; set; }
    }
}