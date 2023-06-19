using System.Security.Claims;

namespace EduArticles.UI.Extensions;

public static class UserExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        => Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
}
