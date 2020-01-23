using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponse>
	{
		private readonly IRepository<User> _repository;
		private readonly IMapper _mapper;

		public CreateUserHandler(IRepository<User> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<User>(request);

			using (var hmac = new HMACSHA512())
			{
				user.PasswordSalt = hmac.Key;
				user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
			}

			await _repository.Create(user);
			return _mapper.Map<UserResponse>(user);
		}
	}
}
