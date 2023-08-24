using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Models;

namespace WebApplication3.Requests
{
    public class UserRequest : IRequest<UserResponse>
    {
        [Required, MinLength(2, ErrorMessage = "Min length is 2")]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password), Required]
        public string Password { get; set; }
    }
}
