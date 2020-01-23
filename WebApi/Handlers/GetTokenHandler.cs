using AutoMapper;
using Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Queries;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class GetTokenHandler : IRequestHandler<GetTokenQuery, TokenResponse>
	{
		private readonly IRepository<User> _repository;
		private readonly IConfiguration _config;

		public GetTokenHandler(IRepository<User> repository, IConfiguration config)
		{
			_repository = repository;
			_config = config;
		}

		public async Task<TokenResponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
		{
			TokenResponse token = null;

			var user = await _repository.FirstOrDefault(u => u.Username.ToLower() == request.Username.ToLower());

			if (user != null && VerifyUserPassword(user, request.Password))
			{
				token = new TokenResponse()
				{
					Token = this.GenerateToken(user)
				};
			}

			return token;
		}

		private string GenerateToken(User user)
		{
			var key = Convert.FromBase64String(_config["Jwt:Key"]);
			var securityKey = new SymmetricSecurityKey(key);
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Email, user.Email)
			};

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Issuer"],
				claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private static bool VerifyUserPassword(User user, string password)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != user.PasswordHash[i]) return false;
				}
			}

			return true;
		}
	}
}
