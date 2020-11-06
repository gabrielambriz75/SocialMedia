using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);

            var response = new ApiResponse<IEnumerable<PostDTO>>(postsDTO);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            var postDTO = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            await _postRepository.InsertPost(post);

            postDTO = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);

            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            post.PostId = id;
            var result = await _postRepository.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _postRepository.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(result);
        }

    }
}
