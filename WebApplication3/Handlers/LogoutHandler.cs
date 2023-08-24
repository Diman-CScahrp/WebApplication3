using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Requests;

namespace WebApplication3.Handlers
{
    public class LogoutHandler : IRequestHandler<LogoutRequest, bool>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
