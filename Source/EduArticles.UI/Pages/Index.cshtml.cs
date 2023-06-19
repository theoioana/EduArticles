using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduArticles.BusinessLogic.Community.Queries;
using EduArticles.BusinessLogic.User.Queries;
using EduArticles.Models.Entities;
using EduArticles.UI.Extensions;

namespace EduArticles.UI.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IMediator mediator;

    public IndexModel(IMediator mediator)
        => this.mediator = mediator;

    public async Task<IActionResult> OnGet()
    {
        var communitiesResponse = await this.mediator.Send(new GetAllCommunitiesQuery());
        this.Communities = communitiesResponse.Data;

        var feedResponse = await this.mediator.Send(new GetUserFeedQuery
        {
            Id = User.GetUserId()
        });
        this.Feed = feedResponse.Data;

        return Page();
    }

    public IEnumerable<CommunityEntity> Communities { get; set; }
    public IEnumerable<PostEntity> Feed { get; set; }
}
