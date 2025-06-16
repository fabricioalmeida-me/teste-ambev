namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;

public class GetAllUsersResponse
{
    public IReadOnlyCollection<GetAllUsersResponseItem> Data { get; set; } = [];
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}