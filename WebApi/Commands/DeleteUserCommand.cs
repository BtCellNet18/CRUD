using MediatR;
using WebApi.Responses;

namespace WebApi.Commands
{
	public class DeleteUserCommand : IRequest<UserResponse>
	{
		public int Id { get; }

		public DeleteUserCommand(int id)
		{
			Id = id;
		}
	}
}
