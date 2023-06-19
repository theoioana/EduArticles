namespace EduArticles.BusinessLogic.Interfaces;

public interface IUser
{
    public Guid Id { get; }
    public string UserName { get; }
    public bool IsAuthenticated { get; }
}
