﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using WebAPI.Filters;
using WebAPI.Helper;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1

{
    
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [SwaggerOperation(Summary = "Retrives all posts")]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]PaginationFilter paginationFilter)
        {
            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var posts = await _postService.GetAllPostsAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize);
            var totalRecords = await _postService.GetAllCountAsync();

            return Ok(PaginationHelper.CreatePageResponse(posts, validPaginationFilter, totalRecords));
        }

        [SwaggerOperation(Summary = "Retrives a sprcific post by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Create a new post")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePostDto newPost)
        {
            var post = await _postService.AddNewPostAsync(newPost);
            return Created($"api/posts/{post.Id}", new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Update a existing post")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatePostDto updatePost)
        {
            await _postService.UpdatePostAsync(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete a specific post")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        [SwaggerOperation(Summary ="Search post by title")]
        [HttpGet("Search/{title}")]
        public async Task<IActionResult> GetAsync(string title)
        {
            var posts = await _postService.SearchPostByTitleAsync(title);
            return Ok(posts);
        }
    }
}
