using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduArticles.BusinessLogic.Post.Commands;
using EduArticles.BusinessLogic.Post.Queries;
using EduArticles.UI.Extensions;
using System.ComponentModel.DataAnnotations;

namespace EduArticles.UI.Pages.Post;

[Authorize]
public class EditModel : PageModel
{
    private readonly IMediator mediator;

    public EditModel(IMediator mediator)
        => this.mediator = mediator;

    public async Task<IActionResult> OnGet(Guid id)
    {
        var response = await this.mediator.Send(new GetPostByIdQuery
        {
            Id = id
        });

        if (response.IsError)
        {
            return Redirect(RouteConstants.Error);
        }
        else if (response.Data.UserId != User.GetUserId())
        {
            return Redirect(RouteConstants.Error);
        }

        this.Id = response.Data.Id;
        this.Title = response.Data.Title;
        this.Text = response.Data.Content;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var response = await this.mediator.Send(new UpdatePostCommand
            {
                Id = this.Id,
                Title = this.Title,
                Content = this.Text,
                Image = this.Image == null ? null : await this.Image.GetBytes()
            });

            if (response.IsError)
            {
                return Redirect(RouteConstants.Error);
            }

            return RedirectToPage(RouteConstants.PostView, new { id = response.Data });
        }
        return Page();
    }

    [Required]
    [BindProperty]
    public Guid Id { get; set; }
    [BindProperty]
    public IFormFile Image { get; set; }
    [Required]
    [BindProperty]
    public string Title { get; set; }
    [Required]
    [BindProperty]
    public string Text { get; set; }
}
