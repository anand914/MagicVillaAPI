using AutoMapper;
using Magic_VillaAPI.Data;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Models.Dto;
using Magic_VillaAPI.Repository.IRepos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_VillaAPI.Repository
{
    public class UserRepos : IUserRepository
    {
        private readonly VillaContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretkey;

        public UserRepos(VillaContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager, 
            IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _context.ApplicationUsers.Where(x => x.UserName == username).FirstOrDefault();
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == model.UserName.ToLower());
                bool isValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (user == null && isValid == false)
                {
                    return new LoginResponseDTO()
                    {
                        Token = "",
                        User = null
                    };
                }
                //if user was found generate JWT Token
                var roles = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretkey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = _mapper.Map<UserDTO>(user),
                   // Role = roles.FirstOrDefault()
                };
                return loginResponseDTO;
            }   
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<UserDTO> Register(RegisterRequestDTO model)
        {
            ApplicationUser user = new()
            {
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                Name = model.Name
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("customer"));
                    }
                    await _userManager.AddToRoleAsync(user, "admin");
                    var userToReturn = _context.ApplicationUsers
                        .FirstOrDefault(u => u.UserName == model.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new UserDTO();
        }
    }
}
