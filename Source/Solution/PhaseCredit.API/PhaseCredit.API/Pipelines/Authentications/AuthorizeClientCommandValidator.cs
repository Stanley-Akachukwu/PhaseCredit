using FluentValidation;
using PhaseCredit.API.Commands.Authentications;

namespace PhaseCredit.API.Pipelines.Authentications
{
    public class AuthorizeClientCommandValidator : AbstractValidator<AuthorizeClientCommand>
    {
        public AuthorizeClientCommandValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ClientSecret).NotEmpty();
        }
    }
}
