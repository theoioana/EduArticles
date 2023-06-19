using MediatR;
using Microsoft.EntityFrameworkCore;
using EduArticles.BusinessLogic.Interfaces;
using EduArticles.BusinessLogic.Validation;
using EduArticles.Models.Entities;

namespace EduArticles.BusinessLogic.Community.Queries;

public record GetCommunityByNameQuery : IRequest<Result<CommunityEntity>>
{
    public string Name { get; init; }
}

internal class GetSubredditByNameHandler : IRequestHandler<GetCommunityByNameQuery, Result<CommunityEntity>>
{
    private readonly IDataContext context;

    public GetSubredditByNameHandler(IDataContext context)
        => this.context = context;

    public async Task<Result<CommunityEntity>> Handle(GetCommunityByNameQuery request, CancellationToken cancellationToken)
    {
        var community = await this.context.Communities
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (community is null)
        {
            return Result<CommunityEntity>.Failed("Not found");
        }

        return Result<CommunityEntity>.Ok(community);
    }
}
