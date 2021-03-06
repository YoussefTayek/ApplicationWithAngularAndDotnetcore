

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForDtoRegister userForDtoRegister)
        {          
           userForDtoRegister.Username = userForDtoRegister.Username.ToLower();

           if(await _repo.UserExists(userForDtoRegister.Username))
                return BadRequest("Username already exists");

           var userToCreate = _mapper.Map<User>(userForDtoRegister);

           var CreatedUser = await _repo.Register(userToCreate,userForDtoRegister.Password);

           var userToReturn = _mapper.Map<UserForDetailedDto>(CreatedUser);

            return CreatedAtRoute("GetUser", new {Controller="users", id = CreatedUser.Id}, userToReturn) ;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForDtoLogin userForDtoLogin)
        {
                var userFromRepo = await _repo.Login(userForDtoLogin.Username.ToLower(), userForDtoLogin.Password);
                if(userFromRepo == null)
                    return Unauthorized();
                
                var claims = new []
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));
                
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDeScriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                }; 

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDeScriptor);

                var user = _mapper.Map<UserForListDto>(userFromRepo);

                return Ok(new {
                   user, token = tokenHandler.WriteToken(token)
                });
        }
    }   
    
}