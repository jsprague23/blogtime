using Microsoft.AspNetCore.Mvc;
using API_Users.Repositories;
using API_Users.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace API_Users.Controllers
{
  [Route("api/[controller]")]
  public class PostController : Controller
  {
    private readonly PostRepository _db;
    public PostController(PostRepository repo)
    {
      _db = repo;  
    }
    [HttpPost]
    [Authorize]
    public Post CreatePost([FromBody]Post newPost)
    {
      if(ModelState.IsValid)
      {
        var user = HttpContext.User;
        newPost.AuthorId = user.Identity.Name;
        return _db.CreatePost(newPost);
      }
      return null;
    }
    //get all posts
    [HttpGet]
    public IEnumerable<Post> GetAll()
    {
      return _db.GetAll();
    }
    //get post by id
    [HttpGet("{id}")]
    public Post GetById(int id)
    {
      return _db.GetbyPostId(id);
    }
    //get post by author
    [HttpGet("author/{id}")]
    public IEnumerable<Post> GetByAuthorId(int id)
    {
      return _db.GetbyAuthorId(id);
    }
    //edit post
    [HttpPut("{id}")]
    public Post EditPost(int id, [FromBody]Post newPost)
    {
      return _db.EditPost(id, newPost);
    }
  }
}