using AutoMapper;
using Conteo_y_recaudo.Dto;
using Conteo_y_recaudo.Entities.Identity;
using Conteo_y_recaudo.Errors;
using Conteo_y_recaudo.Extensions;
using Conteo_y_recaudo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
     
        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
        ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            return new UsuarioDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Nombre = user.Nombre
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UsuarioDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Nombre = user.Nombre
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Correo electrónico ya se encuentra registrado" }
                });
            }

            var user = new Usuario
            {
                Nombre = registerDto.Nombre,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Direccion = registerDto.Direccion
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UsuarioDto
            {
                Nombre = user.Nombre,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

    }
}
