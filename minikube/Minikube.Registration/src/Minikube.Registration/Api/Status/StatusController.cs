using Microsoft.AspNetCore.Mvc;

namespace Minikube.Registration.Api.Status
{
    [Route("api/v1")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet("status")]
        public ActionResult Get()
        {
            return Ok(new { status = "ok", instanceId = Startup.InstanceId, version = "1.2.0.0" });
        }
    }
}
