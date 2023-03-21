using AllProjects.Models;

namespace AllProjects.Interface
{
    public interface IHolidayRegistrationService
    {
        /// <summary>
        /// Return holiday registartions for a given employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>A list of registrations</returns>
        Task<IEnumerable<HolidayRegistrationEntity>> GetHolidayRegistartionsAsync();
    }
}
