using hrd_backend.Interface;
using hrd_backend.Model;
using hrd_backend.Model.API;
using hrd_backend.Model.Job_Description;
using hrd_backend.Model.Personnel_R;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDescription : ControllerBase
    {

        public IJDRepo _dbRepo;

        public JobDescription(IJDRepo dbRepo)
        {
            _dbRepo = dbRepo;
        }

        [HttpGet]
        [Route("{uniqueKey}")]
        public async Task<IActionResult> GetJD([FromRoute] string uniqueKey)
        {
            try
            {
                JD pr = new JD();

               var jd =  _dbRepo.GetJD(pr, uniqueKey);
                var duty = _dbRepo.GetJD_Duty(pr.refNo);
                var edu = _dbRepo.GetJD_Edu(pr.refNo);
                var exp = _dbRepo.GetJD_Exp(pr.refNo);
                var resp = _dbRepo.GetJD_Resp(pr.refNo);
                var skill = _dbRepo.GetJD_Skill(pr.refNo);

                pr.duty = duty;
                pr.education = edu;
                pr.experience = exp;
                pr.responsibility = resp;
                pr.skills = skill;

              

                return Ok(new APIResponse<JD>()
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
        public async Task<IActionResult> AddJD([FromBody] AddJD jd)
        {

            try
            {

                var refNo = _dbRepo.AddJD(jd);

                _dbRepo.AddJD_Exp(jd, refNo);
                _dbRepo.AddJD_Edu(jd, refNo);
                _dbRepo.AddJD_Duty(jd, refNo);
                _dbRepo.AddJD_Resp(jd, refNo);
                _dbRepo.AddJD_Skill(jd, refNo);


                return Ok(new APIResponse<AddJD>()
                {
                    status_code = 200,
                    message = $"Successfully inserted. Reference Number: {refNo}",
                    result = jd
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
        public async Task<IActionResult> UpdateHod([FromBody] UpdateData jd)
            {

                try
                {
                var status = "Completed by Approver";

                    _dbRepo.UpdateHodJD(jd, status);

                    


                    return Ok(new APIResponse<UpdateData>()
                    {
                        status_code = 200,
                        message = $"Successfully updated.",
                        result = jd
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
        //[HttpPut]
        //[Route("HR")]
        //public async Task<IActionResult> UpdateHR([FromBody] UpdateData jd)
        //{

        //    try
        //    {
        //        var status = "Completed by Approver";

        //        _dbRepo.UpdateHRJD(jd, status);




        //        return Ok(new APIResponse<UpdateData>()
        //        {
        //            status_code = 200,
        //            message = $"Successfully updated.",
        //            result = jd
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new APIResponse<string>()
        //        {
        //            status_code = 400,
        //            message = "Something is wrong!",
        //            result = ex.Message
        //        });
        //    }


        //}
    }
}
