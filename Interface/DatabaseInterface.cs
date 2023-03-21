using AllProjects.DataContext;
using AllProjects.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace AllProjects.Interface
{
    public class DatabaseInterface : IDatabaseInterface
    {
        private readonly IDbContextFactory<SQLDataContext> _contextFactory;

        public DatabaseInterface(IDbContextFactory<SQLDataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using (SQLDataContext context = _contextFactory.CreateDbContext())
            {
                return await context.customers.ToListAsync();
            }
            //throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomer(int customerid)
        {
            using (SQLDataContext context = _contextFactory.CreateDbContext())
            {
                return await Task.Run(() => context.customers.FirstOrDefault(i => i.Id == customerid));
            }
            
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomerStoredStructure(int customerid = 0)
        {
            using (SQLDataContext context = _contextFactory.CreateDbContext())
            {
                var parameter = new List<SqlParameter>();

                if(customerid == 0)
                    parameter = context.customers.Select(i => new SqlParameter("@ID", i.Id)).ToList();
                else
                    parameter.Add(new SqlParameter("@ID", customerid));

                //var test =  context.Database.SqlQueryRaw<Customer>("exec GetCustomers").AsEnumerable();

                //var test2 = context.Database.SqlQuery<Customer>($"exec GetCustomersByID @ID = {parameter}");

                var cus = context.customers.FromSqlRaw(@"exec GetCustomersByID @ID", parameter.ToArray()).AsEnumerable();

                var order = context.orders.FromSqlRaw(@"exec GetCustomerOrdersByID @ID", parameter.ToArray()).AsEnumerable();

                var orders = context.orders.FromSqlInterpolated<Order>($"exec GetCustomerOrdersByID @ID = {parameter.ToArray()}").AsEnumerable();

                var cusDTO = cus.Select(i => new CustomerDTO() { City = i.City, Country = i.Country, Id= i.Id, FirstName = i.FirstName, LastName = i.LastName, Phone = i.Phone,  });

                //return await Task.Run(() => context.customers.FromSqlRaw($"exec GetCustomersByID @ID", parameter.ToArray()).AsEnumerable());

                return await Task.Run(() => cusDTO);
            }
                
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrders(DateTime? OrderDate = null)
        {
            using (SQLDataContext context = _contextFactory.CreateDbContext())
            {
                if (OrderDate == null)
                    OrderDate = DateTime.Now;

                 return await context.orders.Where(i => i.OrderDate == OrderDate).ToListAsync();
            }
            //throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteStoredProcedure<T>(string spName, List<SqlParameter> parameters) where T : class
        {
            using (SQLDataContext context = _contextFactory.CreateDbContext())
            {
                if (parameters != null && parameters.Any())
                {
                    var paramNames = new List<string>();
                    foreach (var param in parameters)
                    {
                        var name = param.ParameterName;
                        if (param.Direction == ParameterDirection.Output)
                        {
                            name = $"{param.ParameterName} OUT";
                        }
                        paramNames.Add(name);
                    }

                    var x = context.Set<T>().FromSqlRaw($"EXEC {spName} {String.Join(",", paramNames.ToArray())}", parameters);

                    return x.ToList();
                }

                return context.Set<T>().FromSqlRaw($"EXEC {spName}").ToList();
            }
                
        }
    }

}
