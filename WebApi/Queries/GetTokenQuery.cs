using MediatR;
using WebApi.Responses;

namespace WebApi.Queries
{
	public class GetTokenQuery : IRequest<TokenResponse>
	{
		public string Username { get; }
		public string Password { get; }

		public GetTokenQuery(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
