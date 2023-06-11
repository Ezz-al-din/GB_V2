using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[HttpGet("getrole")]
        //public async Task<IActionResult> GetUserRoleByEmail([FromHeader]string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null)
        //    {
        //        return NotFound("User not found");
        //    }

        //    var roles = await _userManager.GetRolesAsync(user);

        //    if (roles == null || roles.Count == 0)
        //    {
        //        return BadRequest("User doesn't have any roles assigned");
        //    }


        //    return Ok(new { roles });
        //}


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUer()
        {

            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }

        [HttpPost("createrole")]
        public async Task<ActionResult> CreateRole(AppRole model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }

            return Ok();

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                role=roles[0]
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            if (user.Email == "admin@admin.com")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Parent");
            }

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                role = "Parent"
            };
        }

        [HttpPost("addchild")]
        public async Task<ActionResult<UserDto>> AddChild(RegisterStudentDto registerStudentDto)
        {
            // Get the parent user
            var parent = await _userManager.FindByEmailAsync(registerStudentDto.ParentEmail);
            if (parent == null)
            {
                return NotFound();
            }

            // Check the number of existing child accounts
            var childCount = await _userManager.GetUsersInRoleAsync("Student");
            if (childCount.Count >= 7)
            {
                return BadRequest("Maximum number of child accounts reached");
            }

            var childUser = new AppUser
            {
                DisplayName = registerStudentDto.DisplayName,
                Email = registerStudentDto.Email,
                UserName = registerStudentDto.Email
            };

            var result = await _userManager.CreateAsync(childUser, registerStudentDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            // Assign the "Child" role to the child user
            await _userManager.AddToRoleAsync(childUser, "Student");

            return new UserDto
            {
                DisplayName = childUser.DisplayName,
                Token = _tokenService.CreateToken(childUser),
                Email = childUser.Email,
                role = "Student"
            };
        }





    }
}