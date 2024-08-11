using BankingControlPanel.Application.Authentication;
using BankingControlPanel.Application.Exceptions;
using BankingControlPanel.Domain.UserManagement.Repository;
using BankingControlPanel.Shared.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingControlPanel.Application.Features.UserFeatures.Queries
{
    internal class LogInQueryHandler(IUserRepository users, IOptions<AuthenticationConfig> options) : IQueryHandler<LogInQuery, LogInQueryResult>
    {
        public async Task<LogInQueryResult> Handle(LogInQuery query, CancellationToken cancellationToken)
        {
            var user = await users.OfName(query.Username, cancellationToken);

            if (user == null)
                throw new AppException(AppErrorCodes.UserDoesNotExist);

            if (!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
                throw new AppException(AppErrorCodes.InvalidCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
                         {
                             new Claim(type: ClaimTypes.Name, value: user.Username),
                             new Claim(type: ClaimTypes.Role, value: user.Role.ToString())
                         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LogInQueryResult(tokenString);
        }
    }

    public record LogInQuery(string Username, string Password) : IQuery<LogInQueryResult>;
    public record LogInQueryResult(string Token);
}
