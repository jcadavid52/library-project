using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.Models;

namespace Application.Layer
{
    public class UserService:IUserService 
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
