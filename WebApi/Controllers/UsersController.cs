using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Commands;
using WebApi.Queries;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public UsersController(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
		{
			var query = new GetAllUsersQuery();
			var response = await _mediator.Send(query);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserResponse>> GetById(int id)
		{
			var query = new GetUserByIdQuery(id);
			var response = await _mediator.Send(query);
			return response != null ? (ActionResult) Ok(response) : NotFound();
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
		{
			var command = _mapper.Map<CreateUserCommand>(request);
			var response = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] UpdateUserRequest request)
		{
			request.Id = id;
			var command = _mapper.Map<UpdateUserCommand>(request);
			var response = await _mediator.Send(command);
			return response != null ? (ActionResult) Ok(response) : NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteUserCommand(id);
			var response = await _mediator.Send(command);
			return response != null ? (ActionResult) NoContent() : NotFound();
		}

		////[AllowAnonymous]
		////[HttpPost]
		////[HttpGet]
		////public async Task<ActionResult> VerifyUsername(string username)
		////{
		////	if (await _service.GetByUsername(username) != null)
		////	{
		////		return BadRequest($"Username {username} is already in use.");
		////	}

		////	return Ok();
		////}

		////[AllowAnonymous]
		////[HttpPost]
		////[HttpGet]
		////public async Task<ActionResult> VerifyEmail(string email)
		////{
		////	if (await _service.GetByEmail(email) != null)
		////	{
		////		return BadRequest($"Email {email} is already in use.");
		////	}

		////	return Ok();
		////}
	}
}
