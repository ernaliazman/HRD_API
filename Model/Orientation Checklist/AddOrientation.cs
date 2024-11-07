using System.Dynamic;

namespace hrd_backend.Model.Orientation_Checklist
{
    public class AddOrientation
    {

       // public string status { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string dateJoined { get; set; }
        public string branch { get; set; }
        public string office { get; set; }
        public string floor { get; set; }
        public string purpose { get; set; }

        public string HOD_Emp_Email { get; set; }
        public string HOD_Emp_NameCard { get; set; }
        public string HOD_Emp_Tagline { get; set; }
        public string HOD_Emp_FB { get; set; }
        public string HOD_Emp_ChineseName { get; set; }
        public string HOD_Emp_PhoneNo { get; set; }
        public string HOD_Emp_Desktop { get; set; }
        public string HOD_Emp_Laptop { get; set; }
        public string HOD_Emp_Other { get; set; }

   


        public Guid requesterId { get; set; }
        public string requesterName { get; set; }
        public string requesterDesignation { get; set; }
        public string requesterDept { get; set; }
        // public string requestedDate { get; set; }

        //public Guid verifierId { get; set; }
    }
}
