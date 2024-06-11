namespace PromoCodeFactory.WebHost.Models.ModelsIn
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeForCreateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<RoleItemForCreateDto> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}
