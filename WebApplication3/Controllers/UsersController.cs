using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Requests;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] UserRequest user)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(user);
                if (result.Succeeded) return Ok();

                foreach (var error in result.Errors ?? Array.Empty<string>())
                {
                    ModelState.AddModelError("", error);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                bool result = await _mediator.Send(user);
                if (result) return Ok();

                ModelState.AddModelError("", "Invalid username or password");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutRequest());
            return Ok();
        }
    }
}
