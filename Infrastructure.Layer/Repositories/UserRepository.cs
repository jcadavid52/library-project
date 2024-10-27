using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly LibraryDBContext _libraryDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager,
            LibraryDBContext libraryDbContext,
            RoleManager<IdentityRole> roleManager)
        {
            _libraryDbContext = libraryDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task AddUser(User user, string password)
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (!await _roleManager.RoleExistsAsync("Usuario"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Usuario"));
            }

            var registerResult = await _userManager.CreateAsync(user, password);

            var messageError = "";

            if (!registerResult.Succeeded)
            {
                foreach(var error in registerResult.Errors)
                {
                    messageError += error.Description;
                }

                throw new ValidationException(messageError);
            }

            await _userManager.AddToRoleAsync(user, "Usuario");
        }

        public async Task<ResponseLogin> Login(string user, string password)
        {
            var userFind = await _libraryDbContext.Users.FirstOrDefaultAsync(
                u => u.UserName.ToLower() == user.ToLower());


            if(userFind != null)
            {
                var userValid = await _userManager.CheckPasswordAsync(userFind, password);

                if (userValid)
                {
                    var rols = await _userManager.GetRolesAsync(userFind);

                    return new ResponseLogin
                    {
                        UserName = userFind.UserName,
                        Rol = rols.FirstOrDefault(),
                        Success = true

                    };
                }
                else
                {
                    return new ResponseLogin
                    {
                        UserName = "",
                        Rol = "",
                        Success = false

                    };
                }

            }
            else
            {
                return new ResponseLogin
                {
                    UserName = "",
                    Rol = "",
                    Success = false

                };
            }
        }
    }
}
