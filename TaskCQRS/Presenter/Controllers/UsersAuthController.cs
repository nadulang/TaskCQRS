using System.Text;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Presenter.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]

    public class UsersAuthController : ControllerBase
    {
        public UsersAuthController()
        {

        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate(UsersAuth u)
        {
            var users = new List<UsersAuth>(){
                new UsersAuth(){username ="anais", password ="doe"},
                new UsersAuth(){username ="darwin", password ="ug7"},
                new UsersAuth(){username ="nicole", password ="kuiyuy8"},
                new UsersAuth(){username ="gumball", password ="ug7Td90h"},
                new UsersAuth(){username ="richard", password ="OIUG678trfg"}

            };
            var _user = users.Find(e => e.username == u.username);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, _user.username),
                    new Claim(ClaimTypes.Sid, _user.password)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secretnya kurang panjaaaaaangggggg banget")), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            var tokenResponse = new
            {
                token = tokenHandler.WriteToken(token),
                user = _user.username
            };

            return Ok(tokenResponse);
        }
    }
}
