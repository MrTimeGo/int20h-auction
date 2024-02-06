using FluentValidation;

namespace Auction.WebApi.Validatiors;

public class WeatherValidator : AbstractValidator<WeatherForecast>
{
    public WeatherValidator()
    {
        RuleFor(w => w.TemperatureC).GreaterThan(100);
    }
}
