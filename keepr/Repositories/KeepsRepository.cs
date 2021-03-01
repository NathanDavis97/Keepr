using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Keep> GetAll()
    {
      string sql = @"
        SELECT 
        keep.*,
        profile.*
        FROM keeps keep
        JOIN profiles profile ON keep.creatorId = profile.id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

        internal Keep GetById(int id)
    {
      string sql = @"
        SELECT 
        keep.*,
        profile.*
        FROM keeps keep
        JOIN profiles profile ON keep.creatorId = profile.id
        WHERE keep.id = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; },  new {id}, splitOn: "id").FirstOrDefault();
    }

    internal int Create(Keep keepData)
    {
      string sql = @"
        Insert Into Keeps
        (name, description, keeps, img, views, creatorId)
        Values
        (@Name, @Description, @Keeps, @Img, @Views, @CreatorId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, keepData);
    }

    internal Keep edit(Keep editData)
    {
      string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        keeps = @Keeps,
        views = @Views
        WHERE id = @Id;";
      _db.Execute(sql, editData);
      return editData;
    }

    internal void Delete(int id)
    {
      string sql = @"
        DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      select keep.*,
      vk.id as VaultKeepId
      FROM vaultkeeps vk
      JOIN keeps keep on keep.id = vk.keepId
      WHERE vaultId = @VaultId";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId });

    }

    internal IEnumerable<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql = @"
      select keep.*,
      prof.id as ProfileId
      FROM profiles prof
      JOIN keeps keep on keep.creatorId = prof.id
      WHERE creatorId = @ProfileId";
      return _db.Query<ProfileKeepViewModel>(sql, new { profileId });

    }
  }
}