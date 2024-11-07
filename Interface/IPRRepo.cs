using hrd_backend.Model.Personnel_R;

namespace hrd_backend.Interface
{
    public interface IPRRepo
    {

        public PRDetails GetPR(PRDetails pr, string refNo);
        public List<string> GetPRDetail_Job(string refNo);
        public List<string> GetPRDetail_Personal(string refNo);
        public string AddPRData(AddPR tr);
        public void AddPR_Job(AddPR tr, string refNo);
        public void AddPR_Personal(AddPR tr, string refNo);
    }
}
