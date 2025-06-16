using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public class GetAllUsersValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.Size).InclusiveBetween(1, 100);
    }
}