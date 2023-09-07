using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGE.Data.Entity;
using SGE.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthRepository(UserManager<User> userManager, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CheckUser(string Email)
        {
            var userCheck = await _userManager.FindByEmailAsync(Email);
            if (userCheck == null)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> SignupUser(UserSignupDto UserDto)
        {
            var user = new User
            {
                UserName = UserDto.UserName,
                FirstName = UserDto.UserName,
                LastName = UserDto.UserName,
                NormalizedUserName = UserDto.Email,
                Email = UserDto.Email,
                PhoneNumber = UserDto.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (UserDto.ProfilePic != null)
            {

                var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "ProfilePic");

                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                string uniqueFileName = Path.GetFileName(UserDto.ProfilePic.FileName);
                var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    await UserDto.ProfilePic.CopyToAsync(filestream);
                    user.Image = "https://localhost:7148/Image/" + uniqueFileName;
                }
            }
            else
            {
                user.Image = "https://localhost:7148/ProfilePic/profilePic.jpg";
            }
            var result = await _userManager.CreateAsync(user, UserDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return true;
            }
            return false;
        }

        public async Task<bool> LoginUser(UserLoginDto UserLogin)
        {
            var user = await _userManager.FindByEmailAsync(UserLogin.Email);
            if (user != null && !user.EmailConfirmed)
            {
                return false;
            }
            if (await _userManager.CheckPasswordAsync(user, UserLogin.Password) == false)
            {
                return false;

            }

            return true;
        }

        public async Task<string> CreateToken(string Email)
        {

            var user = await _userManager.FindByEmailAsync(Email);
            var role = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id),
                new Claim("name", user.UserName),
                new Claim("role", role[0]),
                new Claim("image", user.Image)

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                _configuration.GetSection("AppSettings:Issuer").Value,
                _configuration.GetSection("AppSettings:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
