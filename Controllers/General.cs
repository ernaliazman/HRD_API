using hrd_backend.Interface;
using hrd_backend.Model.API;
using hrd_backend.Model.General;
using hrd_backend.Model.Orientation_Checklist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class General : ControllerBase
    {
        IGeneralRepo _dbRepo;
        public General(IGeneralRepo dbRepo)
        {
            _dbRepo = dbRepo;

        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAllRequest([FromRoute] Guid id)
        {

            try
            {
                var result = _dbRepo.GetAllRequests(id);


                foreach (var request in result)
                {
                    var refNo = request.refNo;
                    if (!string.IsNullOrEmpty(refNo))
                    {
                        if (refNo.Contains("ORT", StringComparison.OrdinalIgnoreCase))
                        {
                            request.requestType = "Orientation List Form";
                        }
                        else if (refNo.Contains("TE", StringComparison.OrdinalIgnoreCase))
                        {
                            request.requestType = "Training Evaluation Form";
                        }
                        else if (refNo.Contains("EMPTR", StringComparison.OrdinalIgnoreCase))
                        {
                            request.requestType = "Employee Transfer Form";
                        }
                        else if (refNo.Contains("PR", StringComparison.OrdinalIgnoreCase))
                        {
                            request.requestType = "Personnel Requisition Form";
                        }
                        else if (refNo.Contains("OJT", StringComparison.OrdinalIgnoreCase))
                        {
                            request.requestType = "On Job Training/familiarisation Programme Form";
                        }
                    }

                }
                return Ok(
                             new APIResponse<List<Request>>()
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
    }
}
