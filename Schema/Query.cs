using AllProjects.DataContext;
using AllProjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AllProjects.Schema
{
    public class Query
    {
        [GraphQLDeprecated("this is no longer used")]
        public string inputData { get; set; } = "All of the projects and lets see";


        public async Task<IEnumerable<Users>> getStrings([FromServices] SQLDataContext context, [GlobalState] string Authorization)
        {
            IEnumerable<Users> courses = await context.MyUser.ToListAsync();

            IEnumerable<Users> courses1 = await context.MyUser.FromSqlRaw<Users>("GetUsers")
                .ToListAsync();

            var param = new SqlParameter("@ID", 1);

            var productDetails = await Task.Run(() => context.MyUser
                            .FromSqlRaw(@"exec GetUsersByID @ID", param).ToListAsync());



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

            return courses;
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
