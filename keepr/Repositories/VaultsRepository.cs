using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Vault> GetAll()
    {
      string sql = @"
        SELECT 
        vault.*,
        profile.*
        FROM vaults vault
        JOIN profiles profile ON vault.creatorId = profile.id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, splitOn: "id");
    }

        internal Vault GetById(int id)
    {
      string sql = @"
        SELECT 
        vault.*,
        profile.*
        FROM vaults vault
        JOIN profiles profile ON vault.creatorId = profile.id
        WHERE vault.id = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; },  new {id}, splitOn: "id").FirstOrDefault();
    }

    internal int Create(Vault vaultData)
    {
      string sql = @"
        Insert Into Vaults
        (name, isPrivate, creatorId, description)
        Values
        (@Name, @IsPrivate, @CreatorId, @Description);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, vaultData);
    }

    internal Vault edit(Vault editData)
    {
      string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id;";
      _db.Execute(sql, editData);
      return editData;
    }

    internal void Delete(int id)
    {
      string sql = @"
        DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal IEnumerable<Vault> GetVaultsByProfileId(string profileId)
    {
      string sql = @"
      SELECT vault.*,
      profile.*
      FROM profiles profile
      JOIN vaults vault on vault.creatorId = profile.id
      WHERE vault.creatorId = @ProfileId";
return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profileId }, splitOn: "id");    }
  }
}