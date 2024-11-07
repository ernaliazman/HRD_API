using hrd_backend.Model.General;

namespace hrd_backend.Interface
{
    public interface IGeneralRepo
    {

        public List<Request> GetAllRequests(Guid id);
    }
}
