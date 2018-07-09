using Microsoft.AspNetCore.Mvc;
using API_Users.Repositories;
using API_Users.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace API_Users.Controllers
{
  [Route("api/[controller]")]
  public class TagController : Controller
  {
    private readonly TagRepository _db;
    public TagController(TagRepository repo)
    {
      _db = repo;  
    }
    [HttpPost]
    [Authorize]
    public Tag CreatePost([FromBody]Tag newTag)
    {
      if(ModelState.IsValid)
      {
        var user = HttpContext.User;
        newTag.AuthorId = user.Identity.Name;
        return _db.CreateTag(newTag);
      }
      return null;
    }
    //get all Tags
    [HttpGet]
    public IEnumerable<Tag> GetAll()
    {
      return _db.GetAll();
    }
    //get Tag by id
    [HttpGet("{id}")]
    public Tag GetById(int id)
    {
      return _db.GetbyTagId(id);
    }
    //get Tag by author
    [HttpGet("author/{id}")]
    public IEnumerable<Tag> GetByAuthorId(int id)
    {
      return _db.GetbyAuthorId(id);
    }
    //edit Tag
    [HttpPut("{id}")]
    public Tag EditTag(int id, [FromBody]Tag newTag)
    {
      return _db.EditTag(id, newTag);
    }
  }
}