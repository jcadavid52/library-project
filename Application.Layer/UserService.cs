using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Layer
{
    public class UserService:IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository,
            IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task AddUser(AddUserDto addUserDto)
        {
            var user = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                PhoneNumber = addUserDto.PhoneNumber,
                Adress = addUserDto.Adress,
                IdEducativeInstitution = addUserDto.IdEducativeInstitution,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                UserName = addUserDto.Email,
                LastName = addUserDto.LastName,
                

            };

           await _userRepository.AddUser(user,addUserDto.Password);
        }

        public string GenerateToken(string username, string rol)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Role,rol)
            }
        ;
            var handleToken = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["ApiSettings:SecretKey"]));
            //var key = Encoding.ASCII.GetBytes()
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                //issuer: _config["Jwt:Issuer"],
                //audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ResponseLoginDto> Login(LoginDto loginDto)
        {
            var responseLogin = await _userRepository.Login(loginDto.User, loginDto.Password);

            if (responseLogin.Success)
            {
                var token = GenerateToken(responseLogin.UserName, responseLogin.Rol);

                return new ResponseLoginDto
                {
                    Token = token,

                };
            }
            else
            {
                return null;
            }
        }


    }
}
