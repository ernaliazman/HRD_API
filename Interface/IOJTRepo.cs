using hrd_backend.Model.OJT;

namespace hrd_backend.Interface
{
    public interface IOJTRepo
    {
        public string AddOJT(AddOJT ojt);

        public void AddOJT_Desc(AddOJT ojt, string refNo);
        public OJTData GetOJT(OJTData ojt, string refNo);

        public List<string> GetOJT_Desc(string refNo);


        public void UpdateHod_OJT(UpdateHodOJT ojt, string refNo);
    }
}
