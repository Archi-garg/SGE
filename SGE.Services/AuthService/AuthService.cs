using SGE.Data.Repository;
using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> Register(UserSignupDto data)
        {
            try
            {
                bool checkUser = await _authRepository.CheckUser(data.Email);
                if (checkUser)
                {
                    var CreateUser = await _authRepository.SignupUser(data);
                    return CreateUser;

                }
                throw new Exception("Email already registered.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> Login(UserLoginDto data)
        {
            try
            {
                bool checkUser = await _authRepository.LoginUser(data);
                if (checkUser)
                {
                    string token = await _authRepository.CreateToken(data.Email);
                    return token;
                }
                throw new Exception("User Not Exist");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
