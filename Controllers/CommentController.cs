using Microsoft.AspNetCore.Mvc;
using API_Users.Repositories;
using API_Users.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace API_Users.Controllers
{
  [Route("api/[controller]")]
  public class CommentController : Controller
  {
    private readonly CommentRepository _db;
    public CommentController(CommentRepository repo)
    {
      _db = repo;  
    }
    [HttpPost]
    [Authorize]
    public Comment CreateComment([FromBody]Comment newComment)
    {
      if(ModelState.IsValid)
      {
        var user = HttpContext.User;
        newComment.AuthorId = user.Identity.Name;
        return _db.CreateComment(newComment);
      }
      return null;
    }
    //get all posts
    [HttpGet]
    public IEnumerable<Comment> GetAll()
    {
      return _db.GetAll();
    }
    //get Comment by id
    [HttpGet("{id}")]
    public Comment GetById(int id)
    {
      return _db.GetbyCommentId(id);
    }
    //get Comment by author
    [HttpGet("author/{id}")]
    public IEnumerable<Comment> GetByAuthorId(int id)
    {
      return _db.GetbyAuthorId(id);
    }
    //edit Comment
    [HttpPut("{id}")]
    public Comment EditComment(int id, [FromBody]Comment newComment)
    {
      return _db.EditComment(id, newComment);
    }
  }
}