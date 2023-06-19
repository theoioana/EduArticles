using Microsoft.EntityFrameworkCore;
using EduArticles.Models.Entities;

namespace EduArticles.BusinessLogic.Interfaces;

public interface IDataContext
{
    DbSet<CommunityEntity> Communities { get; set; }
    DbSet<PostEntity> Posts { get; set; }
    DbSet<ReplyEntity> Replies { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
