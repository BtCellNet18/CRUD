﻿using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
	{
		private readonly IRepository<User> _repository;
		private readonly IMapper _mapper;

		public UpdateUserHandler(IRepository<User> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _repository.GetById(request.Id);

			if (user != null)
			{
				user.FirstName = request.FirstName;
				user.LastName = request.LastName;
				user.Email = request.Email;

				await _repository.Update(user);
			}

			return user == null ? null : _mapper.Map<UserResponse>(user);
		}
	}
}
