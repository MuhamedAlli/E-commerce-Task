using AssignmentForFullStack.Core.DTOs;
using AssignmentForFullStack.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssignmentForFullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<AppUser> _userManager , IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }
        //Create => Register 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisetrUserDTO userDto)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.UserName = userDto.UserName;
                appUser.Email = userDto.Email;
                IdentityResult result = await userManager.CreateAsync(appUser, userDto.Password);
                if(result.Succeeded)
                {
                    return Ok("Account Created Successfully");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }


        //login =>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDto)
        {
            if (ModelState.IsValid)
            {
              AppUser appUser = await userManager.FindByNameAsync(loginUserDto.UserName);
                if (appUser != null) 
                {
                    bool found =await userManager.CheckPasswordAsync(appUser, loginUserDto.Password);
                    if(found==true)
                    {
                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        //get role
                        var roles = await userManager.GetRolesAsync(appUser);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        SigningCredentials signincred =
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                            audience: config["JWT:ValidAudiance"],//url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );
                        appUser.LastLoginTime = DateTime.Now;
                        await userManager.UpdateAsync(appUser); //upadte last Login time to user
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });

                    }
                }
            }
            return Unauthorized();
        }
    }
}
