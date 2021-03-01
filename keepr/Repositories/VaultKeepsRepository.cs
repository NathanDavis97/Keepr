using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {   private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal int Create(VaultKeep vaultKeepData)
    {
      string sql = @"
        Insert Into vaultKeeps
        (vaultId, keepId, creatorId)
        Values
        (@VaultId, @KeepId, @CreatorId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, vaultKeepData);
    }
        
    internal int Delete(int id, string profileId)
    {
      string sql = @"
        DELETE FROM vaultKeeps WHERE (id = @id) AND (creatorId = @profileId) LIMIT 1";
      var result = _db.Execute(sql, new { id, profileId });
      return result;
    }

    }
}