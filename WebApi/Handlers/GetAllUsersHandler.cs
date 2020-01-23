using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Queries;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
	{
		private readonly IRepository<User> _repository;
		private readonly IMapper _mapper;

		public GetAllUsersHandler(IRepository<User> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await _repository.GetAll();
			return _mapper.Map<IEnumerable<UserResponse>>(users);
		}
	}
}
