using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Services.AuthService
{
    public interface IAuthService
    {
        Task<bool> Register(UserSignupDto data);
        Task<string> Login(UserLoginDto data);
    }
}
