using AutoMapper;
using Entities;
using WebApi.Commands;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Mapping
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<User, UserResponse>();
			CreateMap<CreateUserRequest, CreateUserCommand>();
			CreateMap<CreateUserCommand, User>();
			CreateMap<UpdateUserRequest, UpdateUserCommand>();
			CreateMap<UpdateUserCommand, User>();
		}
	}
}
