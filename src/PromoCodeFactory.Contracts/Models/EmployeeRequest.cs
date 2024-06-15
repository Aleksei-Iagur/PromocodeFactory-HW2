namespace PromoCodeFactory.Contracts.Models
{
    /// <summary>
    ///     Модель для изменения данных сотрудника
    /// </summary>
    public class EmployeeRequest
    {
        /// <summary>
        ///     Имя сотрудника
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        ///     Фамилия сотрудника
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        ///     Почтовый ящик сотрудника
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        ///     Идентификаторы ролей сотрудника
        /// </summary>
        public List<Guid> RolesGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AppliedPromocodesCount { get; set; }
    }
}
