using Application.Dto;
using Application.Dto.Dapper;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V3
{
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDapperPostService _dapperPostService;
        public PostsController(IDapperPostService dapperPostService)
        {
            _dapperPostService = dapperPostService;
        }

        [SwaggerOperation(Summary = "Retrieves all posts")]
        [HttpGet]
        public ActionResult Get()
        {
            var posts = _dapperPostService.GetAllPosts();
            return Ok(posts);
        }

        [SwaggerOperation(Summary = "Retrieves a specific post by unique id")]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var post = _dapperPostService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(new Response<DapperPostDto>(post));
        }

        [SwaggerOperation(Summary = "Create a new post")]
        [HttpPost]
        public ActionResult Create(DapperCreatePostDto newPost)
        {
            var post = _dapperPostService.Add(newPost);
            return Created($"api/posts/{post.Id}", new Response<DapperPostDto>(post));
        }

        [SwaggerOperation(Summary = "Update a existing post")]
        [HttpPut]
        public ActionResult Update(DapperUpdatePostDto updatePost)
        {
            _dapperPostService.Update(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete a specific post")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dapperPostService.Delete(id);
            return NoContent();
        }
    }
}
