namespace hrd_backend.Model.Orientation_Checklist
{
    public class AddOrientationHR
    {
        public string refNo { get; set; }
        public string HR_Email { get; set; }
        public string HR_NameCard { get; set; }
        public string HR_Tagline { get; set; }
        public string HR_FB { get; set; }
        //public string HR_ChineseName { get; set; }
        //public string HR_PhoneNo { get; set; }
        public string HR_Desktop { get; set; }
        public string HR_Laptop { get; set; }
        public string HR_Other { get; set; }

        public string HR_PhoneExt { get; set; }
        public string HR_PhonePin { get; set; }
        public string HR_SitArg { get; set; }

        public string welcomingPhoto { get; set; }
        public string orientBrief { get; set; }
        public string compBrief { get; set; }
        public string handbookAdvice { get; set; }
        public string panelClinicInfo { get; set; }
        public string mcNote { get; set; }

        public string tardiness { get; set; }
        public string hraForm { get; set; }
        public string phoneUsage { get; set; }
        public string qessitBrief { get; set; }

        public string workplaceTour { get; set; }
        public string facilityComp { get; set; }
        public string honestyCorner { get; set; }
        public string empItems { get; set; }
        public string fbGroup { get; set; }
        public string fbPost { get; set; }


        public Guid approverId { get; set; }
        public string approverName { get; set; }
        public string approverDesignation { get; set; }
       
    }
}
