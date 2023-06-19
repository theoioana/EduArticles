using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EduArticles.BusinessLogic.Community.Queries;
using EduArticles.BusinessLogic.Post.Commands;
using EduArticles.BusinessLogic.Validation;
using EduArticles.Models.Entities;
using EduArticles.UI.Extensions;
using System.ComponentModel.DataAnnotations;

namespace EduArticles.UI.Pages.Post
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;
        public readonly IStringLocalizer<CreateModel> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CreateModel(IMediator mediator, IStringLocalizer<CreateModel> localizer, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            _localizer = localizer;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGet()
        {
            var response = await this.mediator.Send(new GetAllCommunitiesQuery());
            if (response.IsError)
            {
                return Redirect(RouteConstants.Error);
            }

            if (!response.Data.Any())
            {
                return Redirect(RouteConstants.CommunityCreate);
            }

            this.Communities = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {

                Result<PostEntity> response;

                try
                {
                    var requestCultureFeature = this._httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
                    var currentCulture = requestCultureFeature?.RequestCulture.UICulture.Name;

                    response = await this.mediator.Send(new CreatePostCommand
                    {
                        CommunityId = this.Community,
                        Image = await this.Image.GetBytes(),
                        Title = this.Title,
                        Content = this.Text,
                        Language = currentCulture
                    });
                }
                catch (Exception ex)
                {
                    throw;
                }
                

                if (response.IsError)
                {
                    return Redirect(RouteConstants.Error);
                }

                return RedirectToPage(RouteConstants.PostView, new { response.Data.Id });
            }
            return RedirectToPage();
        }

        [BindProperty]
        public IEnumerable<CommunityEntity> Communities { get; set; }
        [Required]
        [BindProperty]
        public Guid Community { get; set; }
        [Required]
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Text { get; set; }
        [Required]
        [BindProperty]
        public IFormFile Image { get; set; }
    }
}
