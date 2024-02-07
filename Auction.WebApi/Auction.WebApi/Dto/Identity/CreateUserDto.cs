using FluentValidation;

namespace Auction.WebApi.Dto.Identity;

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty();

        RuleFor(u => u.Password)
            .NotNull()
            .MinimumLength(6)
            .Matches("^(?=.*[0-9])(?=.*[a-z])(?=.*[^a-zA-Z\\d\\s:])(?=.*[A-Z]).{6,}$");

        RuleFor(u => u.Email)
            .EmailAddress();
    }
}
