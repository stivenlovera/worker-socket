namespace worker_socket.dtos.socket
{
    public class Lector
    {
        public int idLector { get; set; }
        public DateTime? create_time { get; set; }
        public string nomLector { get; set; }
        public string ipLector { get; set; }
        public string method { get; set; }
        public string endpoint { get; set; }
        public int portLector { get; set; }
        public string userLector { get; set; }
        public string passLector { get; set; }
        public int condicionLector { get; set; }
    }
    public class LectorData<T> : Lector
    {
        public T data { get; set; }
    }
}