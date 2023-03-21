using AllProjects.DataContext;
using AllProjects.Interface;
using AllProjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AllProjects.Schema
{
    public class Query
    {
        [GraphQLDeprecated("this is no longer used")]
        public string inputData { get; set; } = "All of the projects and lets see";

        //private readonly IHolidayRegistrationService _service;
        //private readonly IDbContextFactory<SQLDataContext> _contextFactory;
        private readonly IGetStringsInterface _getStrings;
        private readonly IDatabaseInterface _database;

        public Query(IDatabaseInterface database, IGetStringsInterface getStrings) //IDbContextFactory<SQLDataContext> contextFactory,
        {
            _database = database;
            //_contextFactory = contextFactory;
            _getStrings = getStrings;
        }


        public async Task<IEnumerable<CustomerDTO>> getStrings([FromServices] SQLDataContext context, [GlobalState] string Authorization)
        {
            //using (SQLDataContext context1 = _contextFactory.CreateDbContext())
            //{
            //    var test = await context1.MyUser.ToListAsync();
            //}

            var te = _getStrings.GetUserStrings().Result;

            var res = _database.GetOrders().Result;

            var res1 = _database.GetCustomerStoredStructure(1).Result;

            IEnumerable<Users> courses = await context.MyUser.ToListAsync();

            //IEnumerable<Users> courses1 = await context.MyUser.FromSqlRaw<Users>("GetUsers").ToListAsync();

            var parameter = new List<SqlParameter>();
            var param = new SqlParameter("@ID", 1);
            parameter.Add(param);
            //var productDetails = await Task.Run(() => context.MyUser.FromSqlRaw(@"exec GetUsersByID @ID", parameter.ToArray()).ToListAsync());

            //string authorization = Request.Headers["Authorization"].ToString();

            //IEnumerable<InstructorType> instructors = await context.Instructors
            //    .Where(i => i.FirstName.Contains(term) || i.LastName.Contains(term))
            //    .Select(i => new InstructorType()
            //    {
            //        Id = i.Id,
            //        FirstName = i.FirstName,
            //        LastName = i.LastName,
            //        Salary = i.Salary,
            //    })
            //    .ToListAsync();

            return res1;
        }

        public async Task<int> AddProductAsync([FromServices] SQLDataContext context, Users product)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProductName", product.GUID));
            parameter.Add(new SqlParameter("@ProductDescription", product.CreatedDate));
            parameter.Add(new SqlParameter("@ProductPrice", product.CustomerID));

            // var result = await Task.Run(() => context.MyUser
            //.ExecuteSqlRawAsync(@"exec AddNewProduct @ProductName, @ProductDescription, @ProductPrice, @ProductStock", parameter.ToArray()));

            return 1;
        }
    }
}
