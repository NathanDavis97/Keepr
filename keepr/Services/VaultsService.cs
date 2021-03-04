using System;
using System.Collections.Generic;
using keepr.Models;
using System.Linq;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vrepo;
    private readonly ProfilesRepository _prepo;
    public VaultsService(VaultsRepository vrepo, ProfilesRepository prepo)
    {
      _vrepo = vrepo;
      _prepo = prepo;
    }
    public IEnumerable<Vault> GetAll()
    {
      IEnumerable<Vault> vaults = _vrepo.GetAll();
      return vaults;
    }

    internal Vault GetById(int id, string profileId)
    {
      var data = _vrepo.GetById(id);
      if (data == null)
      {
        throw new Exception("Invalid Id");
      }
      else if(data.IsPrivate == false){ return data; }
      else if(data.CreatorId == profileId) return data;
      else throw new Exception("Vault is Private");
    }

    public Vault Create(Vault newVault)
    {
      newVault.Id = _vrepo.Create(newVault);
      return newVault;
    }

    internal Vault Edit(Vault editData, string profileId )
    {
      Vault Original = GetById(editData.Id, profileId);
      if(Original.CreatorId == profileId)
      {
        editData.Name = editData != null ? editData.Name: Original.Name ;
        editData.IsPrivate= editData != null ? editData.IsPrivate : Original.IsPrivate;
        return _vrepo.edit(editData);
      } else throw new Exception("Access Denied");
    }

    internal string Delete(int id, string userId)
    {
      Vault Original = GetById(id, userId);
      if(Original.CreatorId != userId)
      {
        throw new Exception("Access Denied");
      }
      _vrepo.Delete(id);
      return "Successfully Deleted";
    }

    internal IEnumerable<Vault> GetVaultsByProfileId(string profileId)
    {
      Profile exists = _prepo.GetById(profileId);
      if(exists == null)
      {
        throw new Exception("Invalid Id");
      }
      var results =_vrepo.GetVaultsByProfileId(profileId);
      return results.ToList().FindAll(r => r.IsPrivate == false);
    }
    }
  }
