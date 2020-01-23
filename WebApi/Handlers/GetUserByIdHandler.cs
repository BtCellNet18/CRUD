using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Queries;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
	{
		private readonly IRepository<User> _repository;
		private readonly IMapper _mapper;

		public GetUserByIdHandler(IRepository<User> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _repository.GetById(request.Id);
			return user == null ? null :_mapper.Map<UserResponse>(user);
		}
	}
}
