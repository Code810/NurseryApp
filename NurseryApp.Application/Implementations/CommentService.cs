using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Application.Dtos.Comment;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> Create(CommentCreateDto commentCreateDto)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == commentCreateDto.AppUserId);
            if (appUser == null) throw new CustomException(400, "User not found");
            var blog = await _unitOfWork.blogRepository.GetAsync(b => b.Id == commentCreateDto.BlogId && !b.IsDeleted);
            if (blog == null) throw new CustomException(400, "blog not found");

            var comment = _mapper.Map<Comment>(commentCreateDto);
            comment.AppUserId = commentCreateDto.AppUserId;
            comment.BlogId = commentCreateDto.BlogId;

            await _unitOfWork.commentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return comment.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Parent ID cannot be null");
            var comment = await _unitOfWork.commentRepository.GetAsync(c => c.Id == id && !c.IsDeleted);
            if (comment == null) throw new CustomException(404, "Comment not found");
            comment.IsDeleted = true;
            _unitOfWork.commentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<CommentReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Comment ID cannot be null");
            var comment = await _unitOfWork.commentRepository.GetByWithIncludesAsync(
                p => p.Id == id && !p.IsDeleted,
                p => p.AppUser);

            if (comment == null) throw new CustomException(404, "Comment not found");
            var CommentDto = _mapper.Map<CommentReturnDto>(comment);
            return CommentDto;
        }

        public async Task<IEnumerable<CommentReturnDto>> GetAll(int? blogId)
        {
            var comments = blogId == null
                ? await _unitOfWork.commentRepository.FindWithIncludesAsync(
                    c => !c.IsDeleted,
                    c => c.AppUser)
                : await _unitOfWork.commentRepository.FindWithIncludesAsync(
                    c => !c.IsDeleted && c.BlogId == blogId,
                    c => c.AppUser);

            if (!comments.Any())
            {
                throw new CustomException(404, "Empty Comments List");
            }

            return _mapper.Map<IEnumerable<CommentReturnDto>>(comments);
        }

        public async Task<int> Update(int? id, CommentUpdateDto commentUpdateDto)
        {
            if (id == null) new CustomException(404, "Comment not found");

            var comment = await _unitOfWork.commentRepository.GetAsync(c => c.Id == id && !c.IsDeleted);
            if (comment == null) throw new CustomException(404, "Comment not found");

            _mapper.Map(commentUpdateDto, comment);
            _unitOfWork.commentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();

            return comment.Id;
        }
    }
}
