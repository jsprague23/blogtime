using System.Collections.Generic;
using System.Data;
using API_Users.Models;
using Dapper;

namespace API_Users.Repositories
{
  public class CommentRepository : DbContext
  {
    public CommentRepository(IDbConnection db) : base(db)
    {

    }
    // Create Comment
    public Comment CreateComment(Comment newComment)
    {
      int id = _db.ExecuteScalar<int>(@"
                INSERT INTO comments (body, authorId, postId)
                VALUES (@Body, @AuthorId, @postId);
                SELECT LAST_INSERT_ID();
            ", newComment);
      newComment.Id = id;
      return newComment;
    }
    // GetAll Comment
    public IEnumerable<Comment> getAllComments(int id)
    {
      return _db.Query<Comment>("SELECT * FROM comments WHERE postId=@id", new{id});
    }
    // GetbyAuthor
    public IEnumerable<Comment> GetbyAuthorId(int id)
    {
      return _db.Query<Comment>("SELECT * FROM comments WHERE authorId = @id;", new { id });
    }
    // GetbyId
    public Comment GetbyCommentId(int id)
    {
      return _db.QueryFirstOrDefault<Comment>("SELECT * FROM comments WHERE id = @id;", new { id });
    }
    // Edit
    public Comment EditComment(int id, Comment post)
    {
      post.Id = id;
      var i = _db.Execute(@"
                UPDATE comments SET
                    body = @Body
                WHERE id = @Id
            ", post);
      if (i > 0)
      {
        return post;
      }
      return null;
    }
    // Delete
    public bool Deletecomment(int id)
    {
      var i = _db.Execute(@"
      DELETE FROM comments
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
