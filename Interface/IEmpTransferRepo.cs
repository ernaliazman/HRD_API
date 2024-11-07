using hrd_backend.Model.Employees_Transfer;

namespace hrd_backend.Interface
{
    public interface IEmpTransferRepo
    {
        public string AddEmpTransfer(AddEmpTransfer emp);
        public EmployeeTransferMD GetEmpTr(string refNo);
    }
}
