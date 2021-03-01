using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vrepo;
    public VaultsService(VaultsRepository vrepo)
    {
      _vrepo = vrepo;
    }
    public IEnumerable<Vault> GetAll()
    {
      IEnumerable<Vault> vaults = _vrepo.GetAll();
      return vaults;
    }

    internal Vault GetById(int id)
    {
      var data = _vrepo.GetById(id);
      if(data == null)
      {
        throw new Exception("Invalid Id");
      }
      return data;
    }

    public Vault Create(Vault newVault)
    {
      newVault.Id = _vrepo.Create(newVault);
      return newVault;
    }

    internal Vault Edit(Vault editData, string userId )
    {
      Vault Original = GetById(editData.Id);
      if(Original.CreatorId == userId)
      {
        editData.Name = editData != null ? editData.Name: Original.Name ;
        editData.Description = editData != null ? editData.Description : Original.Description;
        editData.IsPrivate= editData != null ? editData.IsPrivate : Original.IsPrivate;
      }

    }

    internal string Delete(int id, string userId)
    {
      Vault Original = GetById(id);
      if(Original.CreatorId != userId)
      {
        throw new Exception("Access Denied");
      }
      _vrepo.Delete(id);
      return "Successfully Deleted";
    }
  }
}