using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;
using WebApplication3.Requests;

namespace WebApplication3.Headers
{
    public class UsersHandlers : IRequestHandler<UserRequest, UserResponse>
    {
        private UserManager<IdentityUser> _userManager;        

        public UsersHandlers(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponse> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            return new UserResponse
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }
    }
}
