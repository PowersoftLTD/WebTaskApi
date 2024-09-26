//using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AudienceController : ControllerBase
    {
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<IEnumerable<Audience>>> Post(Audience audience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Audience newAudience = AudiencesStore.AddAudience(audience.Name);

            return Ok(newAudience);

        }
    }
}
