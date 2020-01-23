using FluentValidation;
using WebApi.Requests;

namespace WebApi.Validation
{
	public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
	{
		public LoginUserRequestValidator()
		{
			RuleFor(x => x.Password).NotEmpty();
			RuleFor(x => x.Username).NotEmpty();
		}
	}
}
