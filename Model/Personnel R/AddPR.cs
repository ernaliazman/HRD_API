namespace hrd_backend.Model.Personnel_R
{
    public class AddPR
    {
        public string position { get; set; }
        public string company { get; set; }
       // public string status { get; set; }
        public string department { get; set; }

        public string location { get; set; }

        public string dateRequired { get; set; }

        public int numberPersonnel { get; set; }

        public decimal basicSalary { get; set; }

        public string requisitionPurpose { get; set; }

        public string name { get; set; }

        public string manpowerBudget { get; set; }

        public string reasonUnbudget { get; set; }
        public string requestReason { get; set; }
        public string ageLimit { get; set; }

        public string expRequired { get; set; }

        public int yearsRequired { get; set; }

        public string qualificationRequired { get; set; }

        public string disciplineSpecification { get; set; }

        public string computerLiteracyRequired { get; set; }

        public string computerSpecification { get; set; }

        public string ownTransportRequired { get; set; }

        public string[] jobCompetency {  get; set; }

        public string[] personalCompetency { get; set; }

        public string others {  get; set; }

        public Guid requesterId { get; set; }

        public string requesterName { get; set; }

        public string requesterDept { get; set; }

        public string requesterDesignation {  get; set; }

        public string uniqueKey { get; set; }

        //public string requestedDate { get; set; }

        public string verifierEmpId { get; set; }

     //   public string pr_uniqueKey { get; set; }

    }
}
