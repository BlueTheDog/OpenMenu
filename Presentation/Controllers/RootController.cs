using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public ActionResult GetRoot()
        {

            return Ok();
        }
    }
}
