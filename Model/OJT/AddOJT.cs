namespace hrd_backend.Model.OJT
{
    public class AddOJT
    {
        public string staffName { get; set; }
        public string dateJoined { get; set; }

        public string hrDateFinish { get; set; }

        public string staffDateFinish { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string formType { get; set; }
       // public string description { get; set; }

       public string requesterVerification { get; set; }

        public string[] descriptions { get; set; }

        //public string achieveTarget { get; set; }
        //public string reasonifNo { get; set; }
        //public string requesterVerify { get; set; }
        
        public Guid requesterId { get; set; }
        //public string requesterName { get; set; }
        public string requesterDesignation { get; set; }
      //  public string requestedDate { get; set; }

        public string verifierEmpId { get; set; }
       
    }
}
