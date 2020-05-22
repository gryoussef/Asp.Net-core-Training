using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TP2_ASP.NETCORE.Models;

namespace TP2_ASP.NETCORE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly dotnetCoreTpContext db;

        public HomeController(IConfiguration config,dotnetCoreTpContext dbcontext)
        {
            this._config = config;
            this.db = dbcontext;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var currentUser = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault();
            var user = db.Users.Where(x => x.Id == Convert.ToInt32(currentUser.Value)).SingleOrDefault();
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Login([FromBody] Users user)
        {
            var jwtToken = GenerateJSONWebToken(user);
            if (jwtToken == null)
            {
                return Unauthorized("credentials  :" + user.Email+" ; "+user.Password);
            }
            return Ok(jwtToken);
        }
    
      
     
        private string GenerateJSONWebToken(Users userInfo)
        {
            var user = db.Users.Where(x => x.Email == userInfo.Email && x.Password == userInfo.Password).SingleOrDefault();
            if (user == null)
            {
                return null;
            }
            Console.WriteLine(user.Email);
            Console.WriteLine(_config["Jwt:Key"]);
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var expiryDuration = int.Parse(_config["Jwt:ExpiryDuration"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore=DateTime.UtcNow,
                Expires=DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject=new System.Security.Claims.ClaimsIdentity(new List<Claim> { 
                    new Claim("userId",user.Id.ToString()),
                    new Claim("nom",user.Nom.ToString()),
                    new Claim("prenom",user.Prenom.ToString()),
                    new Claim("email",user.Email.ToString()),
                    new Claim("roles",user.Role.ToString()),

                }),
                SigningCredentials= new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)

        };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }

    }
}