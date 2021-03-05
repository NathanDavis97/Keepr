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
    public class KeepsController : ControllerBase
    {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<KeepsController>> Get()
    {
        try
        {
        return Ok(_ks.GetAll());
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

        [HttpGet("{id}")]
    public ActionResult<IEnumerable<KeepsController>> GetById(int id)
    {
        try
        {
        return Ok(_ks.GetById(id));
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }
        [HttpPost]
        [Authorize]

    public async Task<ActionResult<Keep>> Post([FromBody] Keep newKeep)
    {
        try
        {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = userInfo.Id;
        Keep created = _ks.Create(newKeep);
        created.Creator = userInfo;
        return Ok(created);
      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}/edit")]

    public async Task<ActionResult<Keep>> EditValue(int id, [FromBody] Keep editData)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        editData.Creator = userInfo;
        return Ok(_ks.EditValues(editData));

      }
        catch (System.Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }
    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep editData)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        editData.Creator = userInfo;
        return Ok(_ks.Edit(editData, userInfo.Id));

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
        return Ok(_ks.Delete(id, userInfo.Id));
      }
        catch (System.Exception e )
        {
            
            return BadRequest(e.Message);
        }
    }








  }
}