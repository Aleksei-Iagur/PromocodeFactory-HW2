namespace PromoCodeFactory.WebHost.Models
{
    using System;

    public sealed class RoleItemRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
