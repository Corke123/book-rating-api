using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using book_rating_api.data;
using book_rating_api.Dtos;
using book_rating_api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace book_rating_api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest authRequest)
        {
            try
            {
                await authService.Register(authRequest.username, authRequest.password);
                return NoContent();
            }
            catch (AuthenticationException exception)
            {
                return Conflict(exception.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            try
            {
                var jwt = await authService.Login(authRequest.username, authRequest.password);
                return Ok(jwt);
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
        }
    }
}