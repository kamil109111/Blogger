using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Dto.Dapper;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class DapperPostService : IDapperPostService
    {
        private readonly IDapperPostRepository _dapperPostRepository;
        private readonly IMapper _mapper;

        public DapperPostService(IDapperPostRepository dapperPostRepository, IMapper mapper)
        {
            _dapperPostRepository = dapperPostRepository;
            _mapper = mapper;
        }

        public IEnumerable<DapperPostDto> GetAllPosts()
        {
            var posts = _dapperPostRepository.GetAll();
            return _mapper.Map<IEnumerable<DapperPostDto>>(posts);
        }

        public DapperPostDto GetById(int id)
        {
            var post = _dapperPostRepository.GetById(id);
            return _mapper.Map<DapperPostDto>(post);
        }

        public DapperPostDto Add(DapperCreatePostDto newPost)
        {
            if (string.IsNullOrEmpty(newPost.Title))
            {
                throw new Exception("Post can not have an empty title.");
            }

            var post = _mapper.Map<Post>(newPost);
            var result = _dapperPostRepository.Add(post);
            return _mapper.Map<DapperPostDto>(result);
        }

        public void Update(DapperUpdatePostDto updatePost)
        {
            var existingPost = _dapperPostRepository.GetById(updatePost.Id);
            var post = _mapper.Map(updatePost, existingPost);
            _dapperPostRepository.Update(post);
        }

        public void Delete(int id)
        {
            var post = _dapperPostRepository.GetById(id);
            _dapperPostRepository.Delete(post);
        }
    }
}
