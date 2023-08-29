using FlightBookingSystemV5.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Management.Service.Model;
using User.Management.Service.Services;
namespace FlightBookingSystemV5.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        //[Authorize]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //if (!await _userManager.IsEmailConfirmedAsync(user))
                //{
                //    ModelState.AddModelError(string.Empty, "You must confirm your email address before you can log in.");
                //    return StatusCode(StatusCodes.Status400BadRequest,
                //       new Response { Status = "Error", Message = "Please confirm your email!" }); ;
                //}
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                
                //new Claim(JwtRegisteredClaimNames.Jti, user.Id),

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            //return Unauthorized();
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Invalid Credentials, PLease Try Again!   " +
                "" });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            //var userExists = await _userManager.FindByNameAsync(model.Username);
            var userExists1 = await _userManager.FindByNameAsync(model.Email);
            if (/*userExists != null || */userExists1 != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                TwoFactorEnabled=true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await _roleManager.RoleExistsAsync(UserRoles.Airline))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Airline));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //if (await _roleManager.RoleExistsAsync(UserRoles.Airline))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoles.Airline);
            //}
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authenticate", new {token = token, email = user.Email }, Request.Scheme);
            //var message = new Message(new string[] { user.Email! }, "Confirm email", "Please confirm your email\nClick on the below link:\n" + confirmationLink!);
            //_emailService.SendEmail(message);



            //return StatusCode(StatusCodes.Status200OK,
            //    new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        //[HttpGet("ConfirmEmail")]
        //public async Task<IActionResult> ConfirmEmail(string token, string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return StatusCode(StatusCodes.Status200OK,
        //              new Response { Status = "Success", Message = "Email Verified Successfully" });
        //        }
        //    }
        //    return StatusCode(StatusCodes.Status500InternalServerError,
        //               new Response { Status = "Error", Message = "This User Doesnot exist!" });
        //}

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("register_airline")]
        public async Task<IActionResult> RegisterAirline([FromBody] RegisterModel model)
        {
            //var userExists = await _userManager.FindByNameAsync(model.Username);
            var userExists1 = await _userManager.FindByNameAsync(model.Email);
            if (/*userExists != null || */userExists1 != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                TwoFactorEnabled = true,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Airline))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Airline));
            //if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Airline))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Airline);
            }
            //if (await _roleManager.RoleExistsAsync(UserRoles.User))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoles.User);
            //}

            //await _userManager.AddToRoleAsync(user, "Airline");
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authenticate", new { token = token, email = user.Email }, Request.Scheme);
            //var message = new Message(new string[] { user.Email! }, "Confirm email", "PLease confirm your email\nClick on the below link:\n" + confirmationLink!);
            //_emailService.SendEmail(message);

            //return StatusCode(StatusCodes.Status200OK,
            //    new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        //[HttpPost]
        //[Route("login_airline")]
        //public async Task<IActionResult> LoginAirline([FromBody] LoginModel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.UserName);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        }

        //        var token = GetToken(authClaims);

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return Unauthorized();
        //}





        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }

   
}
