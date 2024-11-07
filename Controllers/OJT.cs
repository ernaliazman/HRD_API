using hrd_backend.Interface;
using hrd_backend.Model.API;
using hrd_backend.Model.Employees_Transfer;
using hrd_backend.Model.OJT;
using hrd_backend.Model.Orientation_Checklist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OJT : ControllerBase
    {
        IOJTRepo _dbRepo;
        public OJT(IOJTRepo dbRepo)
        {
            _dbRepo = dbRepo;

        }


        [HttpGet]
        [Route("{refNo}")]
        public async Task<IActionResult> GetOJT([FromRoute] string refNo)
        {
            try
            {
                OJTData ojt = new OJTData();

               var result = _dbRepo.GetOJT(ojt,refNo);
                var desc = _dbRepo.GetOJT_Desc(refNo);

                ojt.descriptions = desc;

                return Ok(
             new APIResponse<OJTData>()
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
                    message = "Something is wrong!",
                    result = ex.Message
                });

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOJT([FromBody] AddOJT ojt)
        {
            try
            {
                var refNo = _dbRepo.AddOJT(ojt);
                _dbRepo.AddOJT_Desc(ojt, refNo);

                return Ok(
             new APIResponse<AddOJT>()
             {
                 status_code = 200,
                 message = $"Successfully inserted. Reference Number: {refNo}",
                 result = ojt
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

        [HttpPut]
        [Route("HOD")]
        public async Task<IActionResult> UpdateHod([FromBody] UpdateHodOJT ojt)
        {
            try
            {
                var status = "Completed by Superior. Waiting for HR";
                 _dbRepo.UpdateHod_OJT(ojt, status);
              

                return Ok(
             new APIResponse<UpdateHodOJT>()
             {
                 status_code = 200,
                 message = $"Successfully updated.",
                 result = ojt
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
