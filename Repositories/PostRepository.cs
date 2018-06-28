using System.Collections.Generic;
using System.Data;
using API_Users.Models;
using Dapper;

namespace API_Users.Repositories
{
  public class PostRepository : DbContext
  {
    public PostRepository(IDbConnection db) : base(db)
    {

    }
    // Create Post
    public Post CreatePost(Post newPost)
    {
      int id = _db.ExecuteScalar<int>(@"
                INSERT INTO posts (title, body, authorId)
                VALUES (@Title, @Body, @AuthorId);
                SELECT LAST_INSERT_ID();
            ", newPost);
      newPost.Id = id;
      return newPost;
    }
    // GetAll Post
    public IEnumerable<Post> GetAll()
    {
      return _db.Query<Post>("SELECT * FROM posts;");
    }
    // GetbyAuthor
    public IEnumerable<Post> GetbyAuthorId(int id)
    {
      return _db.Query<Post>("SELECT * FROM posts WHERE authorId = @id;", new { id });
    }
    // GetbyId
    public Post GetbyPostId(int id)
    {
      return _db.QueryFirstOrDefault<Post>("SELECT * FROM posts WHERE id = @id;", new { id });
    }
    // Edit
    public Post EditPost(int id, Post post)
    {
      post.Id = id;
      var i = _db.Execute(@"
                UPDATE posts SET
                    title = @Title,
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
    public bool DeletePost(int id)
    {
      var i = _db.Execute(@"
      DELETE FROM posts
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
