using MediatR;
using WebApi.Responses;

namespace WebApi.Commands
{
	public class CreateUserCommand : IRequest<UserResponse>
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}
