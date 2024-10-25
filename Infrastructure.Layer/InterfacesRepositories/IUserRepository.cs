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
        Task AddUser(User user,string password);
    }
}
