namespace worker_socket.dtos.hikvision
{
    public class UserInfo
    {
        public string employeeNo { get; set; }
        public string name { get; set; }
        public string userType { get; set; }
        public string gender { get; set; } = null;
        public bool? localUIRight { get; set; } = null;
        public int? maxOpenDoorTime { get; set; } = null;
        public Valid Valid { get; set; }
        public string doorRight { get; set; }
        public List<RightPlan> RightPlan { get; set; }
        public int? groupId { get; set; }
        public string userVerifyMode { get; set; }
    }

    public class Valid
    {
        public bool enable { get; set; }
        public string beginTime { get; set; }
        public string endTime { get; set; }
        public string timeType { get; set; }
    }

    public class RightPlan
    {
        int doorNo { get; set; }
        string planTemplateNo { get; set; }
    }


}