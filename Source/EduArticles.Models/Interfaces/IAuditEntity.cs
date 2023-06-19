namespace EduArticles.Models.Interfaces;

public interface IAuditEntity
{
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Modified { get; set; }
}
