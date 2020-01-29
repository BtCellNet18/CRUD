using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Queries;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Controllers
{
	/// <summary>
	/// AuthController class.
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// AuthController constructor.
		/// </summary>
		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Authenticate User credentials.
		/// </summary> 
		/// <response code="200">OK</response> 
		/// <response code="400">Bad Request</response>  
		/// <response code="500">Internal Server Error</response>  
		[AllowAnonymous]
		[HttpPost]
		[ProducesResponseType(200, Type = typeof(TokenResponse))]
		[ProducesResponseType(400, Type = typeof(void))]
		[ProducesResponseType(500, Type = typeof(void))]
		public async Task<ActionResult> Login([FromBody]LoginUserRequest request)
		{
			var query = new GetTokenQuery(request.Username, request.Password);
			var response = await _mediator.Send(query);
			return response != null ? (ActionResult) Ok(response) : BadRequest();
		}
	}
}