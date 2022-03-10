using FluentValidation;
using PhaseCredit.API.Commands.Authentications;


namespace PhaseCredit.API.Pipelines.Authentications
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
