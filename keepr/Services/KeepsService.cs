using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _krepo;
    private readonly VaultsRepository _vrepo;
    private readonly ProfilesRepository _prepo;
    
    public KeepsService(KeepsRepository krepo, VaultsRepository vrepo, ProfilesRepository prepo)
    {
      _krepo = krepo;
      _vrepo = vrepo;
      _prepo = prepo;
    }
    public IEnumerable<Keep> GetAll()
    {
      IEnumerable<Keep> keeps = _krepo.GetAll();
      return keeps;
    }

    internal Keep GetById(int id)
    {
      var data = _krepo.GetById(id);
      if(data == null)
      {
        throw new Exception("Invalid Id");
      }
      return data;
    }

    public Keep Create(Keep newKeep)
    {
      newKeep.Id = _krepo.Create(newKeep);
      return newKeep;
    }

    internal Keep Edit(Keep editData, string userId )
    {
      Keep Original = GetById(editData.Id);
      if(Original.CreatorId == userId)
      {
        editData.Name = editData != null ? editData.Name: Original.Name ;
        editData.Description = editData != null ? editData.Description : Original.Description;
        editData.Img = editData != null ? editData.Img : Original.Img;
      }else
      {

        editData.Views = editData.Views != 0 ? Original.Views++ : Original.Views;
        editData.Keeps = editData.Keeps != 0 ? Original.Keeps++ : Original.Keeps;
       throw new Exception("Access Denied");

      }
      return _krepo.edit(editData);

    }


    internal string Delete(int id, string userId)
    {
      Keep Original = GetById(id);
      if(Original.CreatorId != userId)
      {
        throw new Exception("Access Denied");
      }
      _krepo.Delete(id);
      return "Successfully Deleted";
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      Vault exists = _vrepo.GetById(vaultId);
      if(exists == null)
      {
        throw new Exception("Invalid Id");
      }
      return _krepo.GetKeepsByVaultId(vaultId);
    }
    internal IEnumerable<Keep> GetKeepsByProfileId(string profileId)
    {
      Profile exists = _prepo.GetById(profileId);
      if(exists == null)
      {
        throw new Exception("Invalid Id");
      }
      return _krepo.GetKeepsByProfileId(profileId);
    }
  }
}