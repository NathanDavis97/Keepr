using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _krepo;
    public KeepsService(KeepsRepository krepo)
    {
      _krepo = krepo;
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
  }
}