using System.Dynamic;

namespace hrd_backend.Model.Orientation_Checklist
{
    public class Orientation
    {
        public Orientation()
        {
            hod = new AddOrientation();  // Initialize hod here
            hr = new AddOrientationHR();  // Initialize hr if needed
        }

        //public string refNo { get; set; }

        public string dateRequested { get; set; }
        public string status { get; set; }

        public string approvedDate { get; set; }


        
        public AddOrientation hod { get; set; }
        public AddOrientationHR hr { get; set; }

        


        //  public Guid requesterId { get; set; }
        

     
    }
}
