using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduArticles.BusinessLogic.Post.Queries;
using EduArticles.Models.Entities;

namespace EduArticles.UI.Pages;

[Authorize]
public class SearchModel : PageModel
{
    private readonly IMediator mediator;

    public SearchModel(IMediator mediator)
        => this.mediator = mediator;

    public async Task<IActionResult> OnGet(string query)
    {
        var response = await this.mediator.Send(new GetPostsByFilter
        {
            Query = query
        });

        if (response.IsError)
        {
            return Redirect(RouteConstants.Error);
        }

        this.Posts = response.Data;
        this.Query = query;
        return Page();
    }

    [BindProperty]
    public string Query { get; set; }
    [BindProperty]
    public IEnumerable<PostEntity> Posts { get; set; }
}
