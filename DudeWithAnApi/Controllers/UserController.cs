using System;
using DudeWithAnApi.Interfaces;
using DudeWithAnApi.RequestDOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DudeWithAnApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest model)
        {
            bool isAuthenticated = _userService.Authenticate(model.Email, model.Password);

            if (!isAuthenticated)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }

            return Ok(new { message = "Sucess" });
        }
    }

}

