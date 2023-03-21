using AllProjects.DataContext;
using AllProjects.Models;
using Microsoft.EntityFrameworkCore;

namespace AllProjects.Interface
{
    public class GetStringsInterface : IGetStringsInterface
    {
        private readonly IHolidayRegistrationService _service;
        private readonly IDbContextFactory<SQLDataContext> _contextFactory;

        public GetStringsInterface(IDbContextFactory<SQLDataContext> contextFactory, IHolidayRegistrationService service)
        {
            _service = service;
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Users>> GetUserStrings()
        {
            using (SQLDataContext context1 = _contextFactory.CreateDbContext())
            {
                //var test = await context1.MyUser.ToListAsync();
                return await Task.Run(() => context1.MyUser.ToListAsync());
            }
            //throw new NotImplementedException();
        }
    }
}
