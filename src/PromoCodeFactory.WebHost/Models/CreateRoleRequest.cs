using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.WebHost.Models
{
    public class CreateRoleRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}