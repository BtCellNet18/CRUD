using MediatR;
using System.Collections.Generic;
using WebApi.Responses;

namespace WebApi.Queries
{
	public class GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
	{
	}
}
