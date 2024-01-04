using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure;
using Common.Infrastructure.Exceptions;
using Common.Models.Queries;
using Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Application.Features.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);
            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var pass = PasswordEncryptor.Encrypt(request.Password);
            if (dbUser.Password != pass)
                throw new DatabaseValidationException("Password is wrong!");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet.");

            var result = _mapper.Map<LoginUserViewModel>(dbUser);
            var claims = new Claim[]
            {
                new (ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new (ClaimTypes.Email,dbUser.EmailAddress),
                new (ClaimTypes.GivenName, dbUser.FirstName),
                new (ClaimTypes.Name, dbUser.UserName),
                new (ClaimTypes.Surname,dbUser.LastName)
            };
            result.Token = GenerateToken(claims);
            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: credentials,
                notBefore: DateTime.Now);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
