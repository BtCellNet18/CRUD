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
	/// <summary>
	/// UserController class
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UsersController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		/// <summary>
		/// UserController constructor
		/// </summary>
		/// <param name="mapper">Mapper</param>
		/// <param name="mediator">Mediator</param>
		public UsersController(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		/// <summary>
		/// Gets all Users.
		/// </summary>
		/// <response code="200">OK</response> 
		/// <response code="401">Unauthorized</response>  
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
		[ProducesResponseType(401, Type = typeof(void))]
		public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
		{
			var query = new GetAllUsersQuery();
			var response = await _mediator.Send(query);
			return Ok(response);
		}

		/// <summary>
		/// Get User.
		/// </summary>
		/// <param name="id"></param>
		/// <response code="200">OK</response> 
		/// <response code="401">Unauthorized</response> 
		/// <response code="404">Not Found</response> 
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(UserResponse))]
		[ProducesResponseType(401, Type = typeof(void))]
		[ProducesResponseType(404, Type = typeof(void))]
		public async Task<ActionResult<UserResponse>> GetById(int id)
		{
			var query = new GetUserByIdQuery(id);
			var response = await _mediator.Send(query);
			return response != null ? (ActionResult) Ok(response) : NotFound();
		}

		/// <summary>
		/// Create User.
		/// </summary> 
		/// <param name="request"></param>
		/// <response code="200">OK</response> 
		[AllowAnonymous]
		[HttpPost]
		[ProducesResponseType(200, Type = typeof(UserResponse))]
		public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
		{
			var command = _mapper.Map<CreateUserCommand>(request);
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Update User.
		/// </summary> 
		/// <param name="id"></param> 
		/// <param name="request"></param>
		/// <response code="200">OK</response> 
		/// <response code="401">Unauthorized</response> 
		/// <response code="404">Not Found</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(200, Type = typeof(UserResponse))]
		[ProducesResponseType(401, Type = typeof(void))]
		[ProducesResponseType(404, Type = typeof(void))]
		public async Task<ActionResult> Update(int id, [FromBody] UpdateUserRequest request)
		{
			request.Id = id;
			var command = _mapper.Map<UpdateUserCommand>(request);
			var response = await _mediator.Send(command);
			return response != null ? (ActionResult) Ok(response) : NotFound();
		}

		/// <summary>
		/// Delete User.
		/// </summary> 
		/// <param name="id"></param> 
		/// <response code="200">OK</response> 
		/// <response code="401">Unauthorized</response> 
		/// <response code="404">Not Found</response> 
		[HttpDelete("{id}")]
		[ProducesResponseType(204, Type = typeof(void))]
		[ProducesResponseType(401, Type = typeof(void))]
		[ProducesResponseType(404, Type = typeof(void))]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteUserCommand(id);
			var response = await _mediator.Send(command);
			return response != null ? (ActionResult) Ok(response) : NotFound();
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
