using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.Models;
using Microsoft.AspNetCore.Identity;
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

        public UserRepository(UserManager<IdentityUser> userManager,
            LibraryDBContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _userManager = userManager;
        }
        public async Task AddUser(User user, string password)
        {
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
        }
    }
}
