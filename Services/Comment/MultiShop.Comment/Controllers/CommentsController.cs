using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly CommentContext _context;

    public CommentsController(CommentContext commentContext)
    {
        _context = commentContext;
    }

    [HttpGet]
    public IActionResult GetAllCommentList()
    {
        try
        {
            var values = _context.UserComments.ToList();
            return Ok(values);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving comments: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCommentById(int id)
    {
        try
        {
            var value = _context.UserComments.Find(id);
            if (value == null)
                return NotFound($"Comment with ID {id} not found.");

            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the comment: {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult CreateComment(UserComment userComment)
    {
        try
        {
            if (userComment == null)
                return BadRequest("Comment data cannot be null.");

            _context.UserComments.Add(userComment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCommentById), new { id = userComment.UserCommentId }, userComment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the comment: {ex.Message}");
        }
    }

    [HttpPut]
    public IActionResult UpdateComment(UserComment userComment)
    {
        try
        {
            if (userComment == null)
                return BadRequest("Comment data cannot be null.");

            if (!_context.UserComments.Any(x => x.UserCommentId == userComment.UserCommentId))
                return NotFound($"Comment with ID {userComment.UserCommentId} not found.");

            _context.UserComments.Update(userComment);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the comment: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteComment(int id)
    {
        try
        {
            var value = _context.UserComments.Find(id);
            if (value == null)
                return NotFound($"Comment with ID {id} not found.");

            _context.UserComments.Remove(value);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the comment: {ex.Message}");
        }
    }

    [HttpGet("CommentListByProductId/{id}")]
    public IActionResult CommentListByProductId(string id)
    {
        try
        {
            var value = _context.UserComments.Where(x => x.ProductId == id).ToList();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving comments by product ID: {ex.Message}");
        }
    }

    [HttpGet("GetActiveCommentCount")]
    public IActionResult GetActiveCommentCount()
    {
        try
        {
            int count = _context.UserComments.Count(x => x.Status == true);
            return Ok(count);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while counting active comments: {ex.Message}");
        }
    }

    [HttpGet("GetPassiveCommentCount")]
    public IActionResult GetPassiveCommentCount()
    {
        try
        {
            int count = _context.UserComments.Count(x => x.Status == false);
            return Ok(count);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while counting passive comments: {ex.Message}");
        }
    }

    [HttpGet("GetTotalCommentCount")]
    public IActionResult GetTotalCommentCount()
    {
        try
        {
            int count = _context.UserComments.Count();
            return Ok(count);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while counting total comments: {ex.Message}");
        }
    }
}
