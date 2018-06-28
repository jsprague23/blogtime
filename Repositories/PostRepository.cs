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
      newPost.Id=id;
      return newPost;
    }
    // GetAll Post
    public IEnumerable<Post> GetAll()
    {
      return _db.Query<Post>("SELECT * FROM posts;");
    }
    // GetbyAuthor
    // GetbyId
    // Edit
    // Delete

    // Add get user favs to user
  }





}
