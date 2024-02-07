using FluentValidation;

namespace Auction.WebApi.Dto.Identity;

public class LoginDto
{
    public string EmailOrUsername { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(u => u.EmailOrUsername)
            .NotNull();

        RuleFor(u => u.Password)
            .NotNull()
            .MinimumLength(6)
            .Matches("^(?=.*[0-9])(?=.*[a-z])(?=.*[^a-zA-Z\\d\\s:])(?=.*[A-Z]).{6,}$");
    }
}