using System.Collections.Generic;
using System.Data;
using API_Users.Models;
using Dapper;

namespace API_Users.Repositories
{
  public class TagRepository : DbContext
  {
    public TagRepository(IDbConnection db) : base(db)
    {

    }
    // Create Post
    public Tag CreateTag(Tag newTag)
    {
      int id = _db.ExecuteScalar<int>(@"
                INSERT INTO tags (name, postId)
                VALUES (@name, @postId);
                SELECT LAST_INSERT_ID();
            ", newTag);
      newTag.Id = id;
      return newTag;
    }
    // GetAll Post
    public IEnumerable<Tag> getAllTags(int postId)
    {
      return _db.Query<Tag>("SELECT * FROM tags WHERE postId = @postId", new {postId});
    }
    // GetbyAuthor
    public IEnumerable<Tag> GetbyAuthorId(int id)
    {
      return _db.Query<Tag>("SELECT * FROM tags WHERE authorId = @id;", new { id });
    }
    // Edit
    // Delete
    public bool DeleteTag(int id)
    {
      var i = _db.Execute(@"
      DELETE FROM tags
      WHERE id = @id
      LIMIT 1;
      ", new { id });
      if (i > 0)
      {
        return true;
      }
      return false;
    }

    // Add get user favs to user
  }





}
