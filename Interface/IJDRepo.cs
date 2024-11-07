using hrd_backend.Model;
using hrd_backend.Model.Job_Description;

namespace hrd_backend.Interface
{
    public interface IJDRepo
    {
        public JD GetJD (JD jd, string refNo);
        public List<string> GetJD_Duty(string uniqueKey);
        public List<string> GetJD_Edu(string uniqueKey);
        public List<string> GetJD_Exp(string uniqueKey);
        public List<string> GetJD_Resp(string uniqueKey);
        public List<string> GetJD_Skill(string uniqueKey);
        public string AddJD(AddJD jd);

        public void AddJD_Duty(AddJD jd, string refNo);
        public void AddJD_Edu(AddJD jd, string refNo);
        public void AddJD_Exp(AddJD jd, string refNo);
        public void AddJD_Resp(AddJD jd, string refNo);
        public void AddJD_Skill(AddJD jd, string refNo);


        public void UpdateHodJD(UpdateData data, string status);
    }
}
