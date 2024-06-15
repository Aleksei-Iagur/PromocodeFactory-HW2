namespace PromocodeFactory.Domain.Converters
{
    using PromoCodeFactory.Contracts.Models;
    using PromoCodeFactory.Core.Domain.Administration;

    public static class EmployeeConverter
    {
        public static Employee CreatreConvert(CreateEmployeeRequest createEmployeeRequest)
        {
            return new Employee
            {
                FirstName = createEmployeeRequest.FirstName,
                LastName = createEmployeeRequest.LastName,
                Email = createEmployeeRequest.Email,
                AppliedPromocodesCount = createEmployeeRequest.AppliedPromocodesCount
            };
        }

        public static Employee UpdateConvert(UpdateEmployeeRequest createEmployeeRequest)
        {
            return new Employee
            {
                Id = createEmployeeRequest.Id,
                FirstName = createEmployeeRequest.FirstName,
                LastName = createEmployeeRequest.LastName,
                Email = createEmployeeRequest.Email,
                AppliedPromocodesCount = createEmployeeRequest.AppliedPromocodesCount
            };
        } 
    }
}
