using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduArticles.BusinessLogic.Reply.Commands;

namespace EduArticles.UI.Pages.Reply;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IMediator mediator;

    public DeleteModel(IMediator mediator)
        => this.mediator = mediator;

    public async Task<IActionResult> OnGet(Guid id)
    {
        var response = await this.mediator.Send(new DeleteReplyCommand
        {
            Id = id
        });

        if (response.IsError)
        {
            return Redirect(RouteConstants.Error);
        }

        return Redirect(RouteConstants.Index);
    }
}
