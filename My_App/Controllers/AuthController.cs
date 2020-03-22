using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using My_App.Data;
using My_App.Dtos;
using My_App.Modles;

namespace My_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo , IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username)) 
                return BadRequest("username already exists");

            var UserToCreate = _mapper.Map<User>(userForRegisterDto);
            
            var creteuser = await _repo.Register(UserToCreate, userForRegisterDto.Password);

            var usertoreturn = _mapper.Map<UserForDetailDto>(creteuser);
            return CreatedAtRoute("GetUser", new { Controller = "Users", id = creteuser.Id }, usertoreturn);
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login (UserForLoginDto userForLogin)
        {
           
            var userfromRepo = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);

            if (userfromRepo == null) 
            return Unauthorized();

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier,userfromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userfromRepo.Name)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            var tokenhandler = new JwtSecurityTokenHandler();



            var token = tokenhandler.CreateToken(tokenDescripter);

            var user = _mapper.Map<Userforlistdto>(userfromRepo);

            return Ok(new
            {
                token = tokenhandler.WriteToken(token),
                user
            });


        }
    }
}