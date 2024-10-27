using Application.Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.InterfacesServices
{
    public interface IUserService
    {
        Task AddUser(AddUserDto addUserDto);
        Task<ResponseLoginDto> Login(LoginDto loginDto);
        string GenerateToken(string username, string rol);
    }
}
