using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseLogin> Login(string user,string password);
        Task AddUser(User user,string password);
        //Task<string> GetIdUserContext();
    }
}
