using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Models;

namespace WebApplication3.Requests
{
    public class LoginRequest : IRequest<bool>
    {
        [Required, MinLength(2, ErrorMessage = "Min length is 2")]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required]
        public string Password { get; set; }
    }
}
