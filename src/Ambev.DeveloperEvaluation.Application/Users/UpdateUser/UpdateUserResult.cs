namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserResult
{
    public Guid Id { get; }

    public UpdateUserResult(Guid id)
    {
        Id = id;
    }
}