using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Queries;
using WebApi.Requests;

namespace WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login([FromBody]LoginUserRequest request)
		{
			var query = new GetTokenQuery(request.Username, request.Password);
			var response = await _mediator.Send(query);
			return response != null ? (ActionResult) Ok(response) : Unauthorized();
		}
	}
}