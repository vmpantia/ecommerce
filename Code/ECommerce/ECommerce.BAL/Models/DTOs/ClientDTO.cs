using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.DTOs
{
    public class ClientDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
