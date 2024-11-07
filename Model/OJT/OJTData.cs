namespace hrd_backend.Model.OJT
{
    public class OJTData
    {
        public string refNo { get; set; }

        public string status { get; set; }
        public string staffName { get; set; }
        public string dateJoined { get; set; }

        public string hrDateFinish { get; set; }

        public string staffDateFinish { get; set; }
        public string company { get; set; }
        public string department { get; set; }

        public string dateRequested { get; set; }
       // public string designation { get; set; }
        public string formType { get; set; }
       // public string description { get; set; }

        public string requesterVerification { get; set; }

         public Guid requesterId { get; set; }

        public string requesterName { get; set; }

        public string requesterDept { get; set; }

        public string requesterDesignation { get; set; }

        public List<string> descriptions { get; set; }

        public string achieveTarget { get; set; }

        public string? reasonifNo { get; set; }

        public string trainerVerification { get; set; }

        public string hodName { get; set; }

        public string hodDesignation { get; set; }

        public string hodDate { get; set; }

        public string hrVerification { get; set; }
        public string hrName { get; set; }

        public string hrDesignation { get; set; }

        public string hrDate { get; set; }


    }
}
