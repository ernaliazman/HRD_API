using hrd_backend.Data;
using hrd_backend.Interface;
using hrd_backend.Model.API;
using hrd_backend.Model.Personnel_R;
using hrd_backend.Model.Training_Evaluation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelRequisition : ControllerBase
    {

        public IPRRepo _dbRepo;

        public PersonnelRequisition(IPRRepo dbRepo)
        {
            _dbRepo = dbRepo;
        }



        [HttpGet]
        [Route("{refNo}")]
        public async Task<IActionResult> GetPR([FromRoute] string refNo)
        {
            try
            {
                PRDetails pr = new PRDetails();

                var result = _dbRepo.GetPR(pr,refNo);
                var job = _dbRepo.GetPRDetail_Job(refNo);

                pr.jobCompetency = job;
                var personal = _dbRepo.GetPRDetail_Personal(refNo);
                pr.personalCompetency = personal;

                return Ok(new APIResponse<PRDetails>()
                {
                    status_code = 200,
                    message = $"Successfully retrieved.",
                    result = pr
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
        public async Task<IActionResult> AddPR([FromBody] AddPR pr)
        {
            try
            {

                var refNo = _dbRepo.AddPRData(pr);

                _dbRepo.AddPR_Job(pr, refNo);
                _dbRepo.AddPR_Personal(pr, refNo);

                return Ok(new APIResponse<AddPR>()
                {
                    status_code = 200,
                    message = $"Successfully inserted. Reference Number: {refNo}",
                    result = pr
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


        [NonAction]
        public async Task<IActionResult> UpdateHodPR([FromBody] AddPR pr)
        {
            try
            {

                var refNo = _dbRepo.AddPRData(pr);

                _dbRepo.AddPR_Job(pr, refNo);
                _dbRepo.AddPR_Personal(pr, refNo);

                return Ok(new APIResponse<AddPR>()
                {
                    status_code = 200,
                    message = $"Successfully inserted. Reference Number: {refNo}",
                    result = pr
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



