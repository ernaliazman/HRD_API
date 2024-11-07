using hrd_backend.Interface;
using hrd_backend.Model.API;
using hrd_backend.Model.Employees_Transfer;
using hrd_backend.Model.Job_Description;
using hrd_backend.Model.Orientation_Checklist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTransfer : ControllerBase
    {
        public IEmpTransferRepo _dbRepo;

        public EmployeeTransfer(IEmpTransferRepo dbRepo) 
        { 
            _dbRepo = dbRepo;
        }

        [HttpGet]
        [Route("{refNo}")]
        public async Task<IActionResult> GetEmpTransfer([FromRoute] string refNo)
        {
            try
            {
                var result = _dbRepo.GetEmpTr(refNo);
                return Ok(
                             new APIResponse<EmployeeTransferMD>()
                             {
                                 status_code = 200,
                                 message = $"Successfully retrieved.",
                                 result = result
                             });
            }



            catch (Exception ex)
            {
                return Ok(new APIResponse<string>()
                {
                    status_code = 400,
                    message = ex.Message,
                    result = ex.Message
                });

            }
        }

[HttpPost]
        public async Task<IActionResult> AddEmpTransfer([FromBody] AddEmpTransfer emp)
        {
            try
            {
                var refNo = _dbRepo.AddEmpTransfer(emp);
                return Ok(
                         new APIResponse<AddEmpTransfer>()
                         {
                             status_code = 200,
                             message = $"Successfully inserted. Reference Number: {refNo}",
                             result = emp
                         });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse<string>()
                {
                    status_code = 400,
                    message = "Something is wrong!",
                    result = ex.Message
                });

            }
           

        }

    }
}
