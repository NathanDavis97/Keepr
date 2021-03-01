using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{  [ApiController]
  [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;
    public VaultsController(VaultsService vs, KeepsService ks)
    {
      _vs = vs;
      _ks = ks;

    }
    [HttpGet]
    public ActionResult<IEnumerable<VaultsController>> Get()
    {
        try
        {
        return Ok(_vs.GetAll());
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

        [HttpGet("{id}")]
        
    public async  Task<ActionResult<IEnumerable<VaultsController>>> GetById(int id)
    {
        try
        {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();        
        return Ok(_vs.GetById(id, userInfo.Id));
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }
        [HttpPost]
        [Authorize]

    public async Task<ActionResult<Vault>> Post([FromBody] Vault newVault)
    {
        try
        {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        Vault created = _vs.Create(newVault);
        created.Creator = userInfo;
        return Ok(created);
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editData)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        editData.Creator = userInfo;
        return Ok(_vs.Edit(editData, userInfo.Id));

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
        return Ok(_vs.Delete(id, userInfo.Id));
      }
        catch (System.Exception e )
        {
            
            return BadRequest(e.Message);
        }
    }
     [HttpGet("{vaultId}/keeps")]
     public ActionResult<IEnumerable<Keep>> GetVaultKeeps(int vaultId)
     {
       try
       {
        return Ok(_ks.GetKeepsByVaultId(vaultId));
      }
       catch (System.Exception e)
       {
           
           return BadRequest(e.Message);
       }
     }









  }
}