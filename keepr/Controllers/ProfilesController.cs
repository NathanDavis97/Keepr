using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{  [ApiController]
  [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
    private readonly ProfilesService _service;

    public ProfilesController(ProfilesService service)
    {
      _service = service;
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        try
        {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }







  }
}