using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public class GetAllUsersResult
{
    public IReadOnlyCollection<User> Data { get; }
    public int TotalItems { get; }
    public int CurrentPage { get; }
    public int TotalPages { get; }

    public GetAllUsersResult(
        IReadOnlyCollection<User> data,
        int totalItems,
        int currentPage,
        int totalPages)
    {
        Data = data;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        TotalPages = totalPages;
    }
}