using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
        {
      _repo = repo;
        }

        public VaultKeep Create(VaultKeep newVK)
        {
      int id = _repo.Create(newVK);
      newVK.Id = id;
      return newVK;
    }
    internal string Delete(int id, string profileId)
    {
      var result = _repo.Delete(id, profileId);
      if (result == 1)
      { return "Successfully Deleted"; }
      else throw new System.Exception("401");
    }

    }
}