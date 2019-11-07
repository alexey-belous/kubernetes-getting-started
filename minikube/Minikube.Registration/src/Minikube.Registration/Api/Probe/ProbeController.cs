using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Minikube.Registration.Api.Probe
{
    [ApiController]
    [Route("api/v1/probe/")]
    public class ProbeController : ControllerBase
    {
        private readonly IStorageAccessibilityChecker _storageAccessibilityChecker;
        public ProbeController(IStorageAccessibilityChecker storageAccessibilityChecker)
        {
            _storageAccessibilityChecker = storageAccessibilityChecker;
        }

        [HttpGet("readiness")]
        public async Task<ActionResult> ReadinessProbe()
        {
            if (await _storageAccessibilityChecker.CheckAccessibility())
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpGet("liveness")]
        public ActionResult LivenessProbe()
        {
            return NoContent();
        }
    }
}