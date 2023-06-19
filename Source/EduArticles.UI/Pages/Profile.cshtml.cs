using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduArticles.BusinessLogic.Post.Queries;
using EduArticles.Models.Entities;

namespace EduArticles.UI.Pages;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly IMediator mediator;

    public ProfileModel(IMediator mediator)
        => this.mediator = mediator;

    public async Task<IActionResult> OnGet(string name)
    {
        var response = await this.mediator.Send(new GetPostsByUserName
        {
            UserName = name
        });

        if (response.IsError)
        {
            return Redirect(RouteConstants.Error);
        }

        this.Posts = response.Data;
        this.UserName = name;
        return Page();
    }

    [BindProperty]
    public string UserName { get; set; }
    [BindProperty]
    public IEnumerable<PostEntity> Posts { get; set; }
}
