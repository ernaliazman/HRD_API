namespace hrd_backend.Model.Job_Description
{
    public class JD
    {
        public string dateRequested {  get; set; }
        public string refNo { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string reportTo { get; set; }

        public List<string> responsibility { get; set; }
        public List<string> duty { get; set; }

        public List<string> experience { get; set; }

        public List<string> education { get; set; }
        public List<string> skills { get; set; }


        public Guid requesterId { get; set; }
        public string requesterName { get; set; }
        public string requesterDesignation { get; set; }

        public Guid approverId { get; set; }
        public string approverName { get; set; }
        public string approverDesignation { get; set; }

        public string approvedDate { get; set; }
        // public string requestedDate { get; set; }

        public string pr_uniqueKey { get; set; }
    }
}
