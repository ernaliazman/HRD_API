namespace hrd_backend.Model.Job_Description
{
    public class AddJD
    {

       // public string status { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string reportTo { get; set; }

        public string[] responsibility { get; set; }
        public string[]  duty { get; set; }

        public string[] experience { get; set; }

        public string[] education { get; set; }
        public string[] skills { get; set; }


        public Guid requesterId { get; set; }
        public string requesterName { get; set; }
        public string requesterDesignation { get; set; }
       // public string requestedDate { get; set; }

        public string pr_uniqueKey { get; set; }
        public string verfierEmpId { get; set; }
    }
}
