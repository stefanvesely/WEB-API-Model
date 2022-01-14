using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using try5000rpg.Models;

namespace try5000rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string Password);
        Task<ServiceResponse<string>> Login(string Username, string password);

        Task<bool> UserExists(string username);
    }
}
