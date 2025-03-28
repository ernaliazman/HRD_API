using hrd_backend.Model.Orientation_Checklist;

namespace hrd_backend.Interface
{
    public interface IOrientationRepo
    {
        public string AddOrientationDetails(AddOrientation tr);
        public Orientation GetOrientationList(string refNo);

        public void UpdateOrtHR(AddOrientationHR tr);
    }
}
