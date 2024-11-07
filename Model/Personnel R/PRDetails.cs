namespace hrd_backend.Model.Personnel_R
{
    public class PRDetails
    {
        public PRDetails() {

            verifier1 = new PR_Verifier();
            verifier2 = new PR_Verifier();
            approver = new PR_Verifier();
            hrd = new PR_HRD();

        }

        public string uniqueKey { get; set; }
        public string refNo { get; set; }
        public string status { get; set; }
        public string dateRequested { get; set; }
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

        public List<string> jobCompetency { get; set; }

        public List<string> personalCompetency { get; set; }

        public string others {  get; set; }

       // public Guid requesterId { get; set; }

        public string requesterName { get; set; }

        public string requesterDept { get; set; }

        public string requesterDesignation { get; set; }

        public PR_Verifier verifier1 { get; set; }

       public PR_Verifier verifier2 { get; set; }

        public PR_Verifier? approver {  get; set; }

       public PR_HRD hrd { get; set; }

        //public class approver
        //{
        //    public string? name { get; set; }

        //    public string? designation { get; set; }

        //    public string? date { get; set; }
        //}

       

    
    }
}
