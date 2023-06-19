﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EduArticles.BusinessLogic.Post.Queries;
using EduArticles.BusinessLogic.Reply.Commands;
using EduArticles.Models.Entities;
using EduArticles.UI.Extensions;
using System.ComponentModel.DataAnnotations;

namespace EduArticles.UI.Pages.Post;

[Authorize]
public class ViewModel : PageModel
{
    private readonly IMediator mediator;

    public ViewModel(IMediator mediator)
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

        this.Post = response.Data;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var response = await this.mediator.Send(new CreateReplyCommand
            {
                PostId = this.Post.Id,
                Content = this.Text
            });

            if (response.IsError)
            {
                return Redirect(RouteConstants.Error);
            }
        }
        return RedirectToPage();
    }

    [BindProperty]
    public PostEntity Post { get; set; }
    [Required]
    [BindProperty]
    public string Text { get; set; }
}
