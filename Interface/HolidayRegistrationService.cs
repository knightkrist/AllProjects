using AllProjects.Models;

namespace AllProjects.Interface
{
    public class HolidayRegistrationService : IHolidayRegistrationService
    {
        public Task<IEnumerable<HolidayRegistrationEntity>> GetHolidayRegistartionsAsync()
        {
            var registrations = new List<HolidayRegistrationEntity> {
                new HolidayRegistrationEntity() { FirstDay = new DateOnly(2022, 12, 1), LastDay = new DateOnly(2022,12,4)},
                new HolidayRegistrationEntity() { FirstDay = new DateOnly(2022, 12, 19), LastDay = new DateOnly(2022,12,31) },
            };

            return Task.FromResult<IEnumerable<HolidayRegistrationEntity>>(registrations);
        }
    }
}
