using hrd_backend.Interface;
using hrd_backend.Model.API;
using hrd_backend.Model.Orientation_Checklist;
using hrd_backend.Model.Training_Evaluation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrientationList : ControllerBase
    {

        public IOrientationRepo _dbRepo;

        public OrientationList(IOrientationRepo dbRepo)
        {
            _dbRepo = dbRepo;
        }



        [HttpGet]
        [Route("{refNo}")]
        public async Task<IActionResult> GetOrientationDetails([FromRoute] string refNo)
        {
            try
            {
                var result = _dbRepo.GetOrientationList(refNo);
                return Ok(
                     new APIResponse<Orientation>()
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
        public async Task<IActionResult> AddTrainingDetails([FromBody] AddOrientation training)
        {
            try
            {

                var refNo = _dbRepo.AddOrientationDetails(training);

                return Ok(new APIResponse<AddOrientation>()
                {
                    status_code = 200,
                    message = $"Successfully inserted. Reference Number: {refNo}",
                    result = training
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

        [HttpPut]
        [Route("HR")]
        public async Task<IActionResult> UpdateHR([FromBody] AddOrientationHR training)
        {
            try
            {

                 _dbRepo.UpdateOrtHR(training);

                return Ok(new APIResponse<AddOrientationHR>()
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
                    message = ex.Message,
                    result = ex.Message
                });
            }

        }
    }
}

