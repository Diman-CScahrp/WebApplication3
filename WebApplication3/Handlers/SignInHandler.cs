using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;
using WebApplication3.Requests;

namespace WebApplication3.Handlers
{
    public class SignInHandler : IRequestHandler<LoginRequest, bool>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignInHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            return result.Succeeded;
        }
    }
}
