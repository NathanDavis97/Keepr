using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{[ApiController]
  [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
        public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVaultKeep)
    {
        try
        {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVaultKeep.CreatorId = userInfo.Id;
        VaultKeep created = _vks.Create(newVaultKeep);
        return Ok(created);
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vks.Delete(id, userInfo.Id));
      }
        catch (System.Exception e )
        {
            
            return BadRequest(e.Message);
        }
    }
    }
}