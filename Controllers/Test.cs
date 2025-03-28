using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hrd_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        // GET: api/<Test>
        [HttpGet]
        public async Task<int[]> Get()
        {
            int n = 8;
            int[] newArray = new int[n +1];

            //Initialise first 2 numbers

            newArray[0] = 0;
            newArray[1] = 1;

            for (int i = 2; i <= n; ++i)
            {
                newArray[i] = newArray[i - 1] + newArray[i - 2];
                newArray[i] = newArray[i - 1] + newArray[i - 2]; //To get 1 index before, and 2 index before

            }

            

            return newArray;
        }

        
    }
}
