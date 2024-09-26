using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Application.Dtos.Blog;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<int> Create(BlogCreateDto blogCreateDto)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == blogCreateDto.AppUserId && !u.IsDeleted);
            if (appUser == null) throw new CustomException(400, "User not found");

            var blog = _mapper.Map<Blog>(blogCreateDto);
            blog.AppUserId = blogCreateDto.AppUserId;

            await _unitOfWork.blogRepository.AddAsync(blog);
            await _unitOfWork.SaveChangesAsync();


            return blog.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Blog ID cannot be null");
            var blog = await _unitOfWork.blogRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (blog == null) throw new CustomException(404, "Blog not found");
            blog.IsDeleted = true;

            Helper.DeleteImage("blogs", blog.FileName);
            _unitOfWork.blogRepository.Update(blog);
            await _unitOfWork.SaveChangesAsync();
            return blog.Id;
        }

        public async Task<BlogReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Blog ID cannot be null");
            var blog = await _unitOfWork.blogRepository.GetAsync(b => b.Id == id && !b.IsDeleted);

            if (blog == null) throw new CustomException(404, "Blog not found");
            var blogDto = _mapper.Map<BlogReturnDto>(blog);
            return blogDto;
        }

        public async Task<IEnumerable<BlogReturnDto>> GetAll(int? count)
        {
            var blogs = await _unitOfWork.blogRepository.GetAllAsyncWithSorting(b => !b.IsDeleted, count);

            if (blogs.Count() <= 0) throw new CustomException(404, "Empty blog List");
            var blogDtos = _mapper.Map<IEnumerable<BlogReturnDto>>(blogs);

            return blogDtos;
        }

        public async Task<BlogListDto> GetAllWithSearch(string? text, int page)
        {
            var blogs = await _unitOfWork.blogRepository.GetAllWithSearch(text, page);

            if (blogs.Count() <= 0) throw new CustomException(404, "Empty blog List");
            var blogDtos = _mapper.Map<IEnumerable<BlogReturnDto>>(blogs);

            BlogListDto blogListDto = new();
            blogListDto.TotalCount = await _unitOfWork.blogRepository.GetAllCount(text);
            blogListDto.Items = blogDtos;
            return blogListDto;

        }



        public async Task<int> Update(int? id, BlogUpdateDto blogUpdateDto)
        {
            if (id == null) throw new CustomException(400, "Blog ID cannot be null");
            var blog = await _unitOfWork.blogRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (blog == null) throw new CustomException(404, "Blog not found");
            if (blogUpdateDto.File != null)
            {
                Helper.DeleteImage("blogs", blog.FileName);
            }
            _mapper.Map(blogUpdateDto, blog);
            _unitOfWork.blogRepository.Update(blog);
            await _unitOfWork.SaveChangesAsync();
            return blog.Id;
        }
    }
}
