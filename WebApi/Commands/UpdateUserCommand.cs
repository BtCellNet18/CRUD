﻿using MediatR;
using WebApi.Responses;

namespace WebApi.Commands
{
	public class UpdateUserCommand : IRequest<UserResponse>
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }
	}
}
