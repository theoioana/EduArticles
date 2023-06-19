using MediatR;
using EduArticles.BusinessLogic.Interfaces;
using EduArticles.BusinessLogic.Validation;
using EduArticles.Models.Entities;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace EduArticles.BusinessLogic.Post.Commands;

public record CreatePostCommand : IRequest<Result<PostEntity>>
{
    public Guid CommunityId { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public byte[] Image { get; init; }
    public string Language { get; init; }
}

internal class CreatePostHandler : IRequestHandler<CreatePostCommand, Result<PostEntity>>
{
    private readonly IDataContext context;
    private readonly HttpClient httpClient;

    public CreatePostHandler(IDataContext context, HttpClient httpClient)
    {
        this.context = context;
        this.httpClient = httpClient;
    }

    public async Task<Result<PostEntity>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        //// Call the local API
        //var apiUrl = "http://127.0.0.1:8000/predict";
        //var requestBody = new { text = request.Content };
        //var json = JsonSerializer.Serialize(requestBody);
        //var content = new StringContent(json, Encoding.UTF8, "application/json");
        //var response = await httpClient.PostAsync(apiUrl, content);

        //// Deserialize the API response
        //var responseContent = await response.Content.ReadAsStringAsync();
        //var responseObject = JsonSerializer.Deserialize<TagsGeneratorApiResponse>(responseContent, new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true,
        //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        //});

        var post = this.context.Posts.Add(new()
        {
            Id = Guid.NewGuid(),
            CommunityId = request.CommunityId,
            Title = request.Title,
            Content = request.Content,
            Image = request.Image,
            Language = request.Language,
            AssignedTags = "tag1 tag2 tag3",
            AssignedCategories = "category1 category2 category3"
        });


        await this.context.SaveChangesAsync(cancellationToken);
        return Result<PostEntity>.Ok(post.Entity);
    }

    private class TagsGeneratorApiResponse
    {
        public string Tags { get; set; }
    }
}
