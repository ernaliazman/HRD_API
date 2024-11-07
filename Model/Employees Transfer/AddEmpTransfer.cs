namespace hrd_backend.Model.Employees_Transfer
{
    public class AddEmpTransfer
    {
        //public string empId {  get; set; }
        //public string status { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string commencementDate { get; set; }
        public string highestQualification { get; set; }
        public string positionInterested { get; set; }

        public string transferDept { get; set; }
        public string workExp { get; set; }
        public string transferReason { get; set; }

        public Guid requesterId { get; set; }
        //public string requesterName { get; set; }
        //public string requesterDesignation { get; set; }
        // public string requestedDate { get; set; }

        public string verifierEmpId { get; set; }

    }
}
