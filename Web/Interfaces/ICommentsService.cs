using Application.Features.Comments;
using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface ICommentsService 
{
	Task<CreateResult<Comment>> CreateCommentForTitle(Guid id, CreateCommentForTitleFromAuthor.Request request); 
}
