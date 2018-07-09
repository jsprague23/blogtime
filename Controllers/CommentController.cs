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
    [HttpGet("post/{id}")]
    public IEnumerable<Comment> GetByPostId(int id)
    {
      return _db.getAllComments(id);
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
    [Authorize]
    public Comment EditComment(int id, [FromBody]Comment newComment)
    {
    if(ModelState.IsValid)
    {
      var User = HttpContext.User;
      newComment.AuthorId = User.Identity.Name;
      return _db.EditComment(id, newComment);
    }
    return null;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public string DeleteComment (int id)
    {
      bool delete = _db.Deletecomment(id);
      if(delete)
      {
        return "Successfully Deleted";
      } 
      return "Something went wrong, please try again.";
    }
  }
}