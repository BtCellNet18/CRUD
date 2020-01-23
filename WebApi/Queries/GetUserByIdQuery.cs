using MediatR;
using WebApi.Responses;

namespace WebApi.Queries
{
	public class GetUserByIdQuery : IRequest<UserResponse>
	{
		public int Id { get; }

		public GetUserByIdQuery(int id)
		{
			Id = id;
		}
	}
}
