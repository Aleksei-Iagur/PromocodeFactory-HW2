namespace PromoCodeFactory.Contracts.Models
{
    public class UpdateEmployeeRequest : EmployeeRequest
    {
        /// <summary>
        ///     Идентификатор обновляемой сущности
        /// </summary>
        public Guid Id { get; set; }
    }
}
