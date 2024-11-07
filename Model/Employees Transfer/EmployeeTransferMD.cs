namespace hrd_backend.Model.Employees_Transfer
{
    public class EmployeeTransferMD
    {
        public EmployeeTransferMD() 
        { 
            hod = new AddEmpTransfer_Hod();
            hr = new AddEmpTransfer_HRD();
            ad = new AddEmpTransfer_AD();
        
        }


        //public string status { get; set; }
        public string refNo { get; set; }
        public string status { get; set; }
        public string dateRequested { get; set; }
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

        public AddEmpTransfer_Hod hod { get; set; }

        public string hod_name { get; set; }

        public string hod_designation { get; set; }

        public string hod_date { get; set; }

       

        public AddEmpTransfer_HRD hr { get; set; }

        public string hr_date { get; set; }
       // public string approved_date { get; set; }
        public AddEmpTransfer_AD ad { get; set; }

        public string ad_date { get; set; }
       



        //public Guid requesterId { get; set; }
        //public string requesterName { get; set; }
        //public string requesterDesignation { get; set; }
        // public string requestedDate { get; set; }

     
    }
}
