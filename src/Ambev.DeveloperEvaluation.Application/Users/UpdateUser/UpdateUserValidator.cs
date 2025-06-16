using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$");

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.Number).GreaterThan(0);
        RuleFor(x => x.ZipCode).NotEmpty();
        RuleFor(x => x.Latitude).NotEmpty();
        RuleFor(x => x.Longitude).NotEmpty();

        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Role).IsInEnum();
    }
}