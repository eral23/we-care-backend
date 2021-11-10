using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Entities.Identity;

namespace WeCare.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> pUserManager;
        private readonly SignInManager<ApplicationUser> pSignInManager;
        private readonly IConfiguration pConfiguration;
        public IdentityController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            pUserManager = userManager;
            pSignInManager = signInManager;
            pConfiguration = configuration;
        }
        //public string Index() { return ""; }
        [HttpPost("register_patient")]
        public async Task<IActionResult> CreatePatient(ApplicationUserRegisterPatientDto model) {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await pUserManager.CreateAsync(user, model.Password);
            var DefaultRole = await pUserManager.AddToRoleAsync(user, "Patient");

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo crear el paciente");
            }
            return Ok();
        }

        [HttpPost("register_specialist")]
        public async Task<IActionResult> CreateSpecialist(ApplicationUserRegisterSpecialistDto model) {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await pUserManager.CreateAsync(user, model.Password);
            var DefaultRole = await pUserManager.AddToRoleAsync(user, "Specialist");

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo crear el especialista");
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDto model)
        {
            var user = await pUserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var check = await pSignInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (check.Succeeded)
                {
                    return Ok(await GenerateToken(user));
                }
            }
            return BadRequest("Acceso no válido a la aplicación");
        }

        [Authorize]
        [HttpGet("refresh_token")]
        public async Task<IActionResult> Refresh()
        {
            var userId = User.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Single().Value;
            var user = await pUserManager.FindByIdAsync(userId);
            
            return Ok(await GenerateToken(user));
        }
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var secretKey = pConfiguration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var roles = await pUserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
    }
}
