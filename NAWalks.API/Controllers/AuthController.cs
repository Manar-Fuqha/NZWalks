using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.userName,
                Email = registerRequestDto.userName
            };
            var IdentityResult = await userManager.CreateAsync(identityUser, registerRequestDto.password);

            if (IdentityResult.Succeeded)
            {

                //add role to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    IdentityResult= await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User was registered! please login.");
                    }
                    

                }

            }


            return BadRequest("Something went wrong");

        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.userName);
            
            if(user != null)
            {
                
              var checkPasswordResult=  await userManager.CheckPasswordAsync(user , loginRequestDto.password);
                if (checkPasswordResult)
                {
                    //Get Roles for this User
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //Create Token
                        var jwtToken= tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto { jwtToken = jwtToken };

                        return Ok(response);
                    }
                    

                   
                }
            }

            return BadRequest("Username or password incorrect");
        }

    }
}
