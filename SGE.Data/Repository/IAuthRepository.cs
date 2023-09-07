using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Repository
{
    public interface IAuthRepository
    {
        Task<bool> CheckUser(string Email);
        Task<bool> SignupUser(UserSignupDto UserDto);
        Task<bool> LoginUser(UserLoginDto loginDto);
        Task<string> CreateToken(string Email);
    }
}
