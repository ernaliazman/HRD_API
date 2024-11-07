namespace hrd_backend.Model.Employees_Transfer
{
    public class AddEmpTransfer_HRD
    {
        public string refNo { get; set; }
        public string receivedBy { get; set; }

        public string receivedDate { get; set; }

        public string interviewDateTime { get; set; }

        public string interviewer {  get; set; }

        public string approvedDate { get; set; }

        public string approvedBy { get;  set; }

        public Guid hr_id { get; set; }
        public string hr_name { get; set; }

        public string hr_designation { get; set; }

        //public string hod_date { get; set; }
    }
}
