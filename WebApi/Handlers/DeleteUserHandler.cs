using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, UserResponse>
	{
		private readonly IRepository<User> _repository;
		private readonly IMapper _mapper;

		public DeleteUserHandler(IRepository<User> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _repository.GetById(request.Id);

			if (user != null)
			{
				await _repository.Delete(user);
			}

			return user == null ? null : _mapper.Map<UserResponse>(user);
		}
	}
}
