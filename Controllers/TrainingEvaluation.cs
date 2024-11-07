using hrd_backend.Interface;
using hrd_backend.Model;
using hrd_backend.Model.API;
using hrd_backend.Model.Training_Evaluation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingEvaluation : ControllerBase
    {
        public IDbRepo _dbRepo;

        public TrainingEvaluation(IDbRepo dbRepo) 
        { 
            _dbRepo = dbRepo;
        }



        [HttpGet]
        [Route("{refNo}")]
        public async Task<IActionResult> GetTrainingDetails([FromRoute] string refNo )
        {
            try
            {
                var result = _dbRepo.GetTrainingDetails(refNo);
                return Ok(
                     new APIResponse<Training>()
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
        public async Task<IActionResult> AddTrainingDetails([FromBody] AddTraining training)
        {
            try
            {

               var refNo = _dbRepo.AddTrainingDetails(training);

                return Ok(new APIResponse<AddTraining>()
                {
                    status_code = 200,
                    message = $"Successfully inserted. Reference Number: {refNo}",
                    result = training
                });
            }
            catch(Exception ex)
            {
                return Ok(new APIResponse<string>()
                {
                    status_code = 400,
                    message = ex.Message,
                    result = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("HOD")]
        public async Task<IActionResult> UpdateHod([FromBody] UpdateData training)
        {
            try
            {
                var status = "Completed by Superior";
                _dbRepo.UpdateHodTR(training, status);

                return Ok(new APIResponse<UpdateData>()
                {
                    status_code = 200,
                    message = $"Successfully updated.",
                    result = training
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
