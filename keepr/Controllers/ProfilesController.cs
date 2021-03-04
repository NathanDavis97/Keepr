using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{  [ApiController]
  [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
    private readonly ProfilesService _service;
    private readonly KeepsService _kservice;
    private readonly VaultsService _vservice;

    public ProfilesController(ProfilesService service, KeepsService kservice, VaultsService vservice)
    {
      _service = service;
      _kservice = kservice;
      _vservice = vservice;

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
     [HttpGet("{profileId}/keeps")]
     public ActionResult<IEnumerable<Profile>> GetKeepsByProfileId(string profileId)
     {
       try
       {
        return Ok(_kservice.GetKeepsByProfileId(profileId));
      }
       catch (System.Exception e)
       {
           
           return BadRequest(e.Message);
       }
     }
          [HttpGet("{profileId}/vaults")]
     public async Task<ActionResult<IEnumerable<Profile>>> GetVaultsByProfileId(string profileId)
     {
       try
       {Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
       string stringId = "";
        if (userInfo == null) { stringId = "0"; }
        else stringId = userInfo.Id;
        return Ok(_vservice.GetVaultsByProfileId(profileId, stringId));
      }
       catch (System.Exception e)
       {
           
           return BadRequest(e.Message);
       }
     }








  }
}