using hrd_backend.Model;
using hrd_backend.Model.Training_Evaluation;

namespace hrd_backend.Interface
{
    public interface IDbRepo
    {

        public Training GetTrainingDetails(string refNo);
        public string AddTrainingDetails(AddTraining tr);
        public int GetRunningNumber(string date, string storePro);

        public void UpdateHodTR(UpdateData data, string status);

        public SuperiorDetails get_verifier(string employee_id);
        public SuperiorDetails get_approver1(string emp_id);
    }
}
