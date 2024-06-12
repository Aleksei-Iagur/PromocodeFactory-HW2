using PromoCodeFactory.Core.Domain.Administration;
using System;

namespace PromoCodeFactory.WebHost.Models
{
    public class EmployeeShortResponse
    {
        public EmployeeShortResponse()
        {
        }

        public EmployeeShortResponse(Employee model)
        {
            Id = model.Id;

            FullName = model.FullName;

            Email = model.Email;
        }

        public Guid Id { get; set; }
        
        public string FullName { get; set; }

        public string Email { get; set; }
    }
}