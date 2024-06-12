using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.WebHost.Models
{
    public class EmployeeResponse
    {
        public EmployeeResponse()
        {
        }

        public EmployeeResponse(Employee model)
        {
            Id = model.Id;

            Email = model.Email;

            Roles = model.Roles.Select(x => new RoleItemResponse()
            {
                Name = x.Name,
                Description = x.Description
            }).ToList();

            FullName = model.FullName;

            AppliedPromocodesCount = model.AppliedPromocodesCount;
        }

        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public List<RoleItemResponse> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}