using AllProjects.Models;

namespace AllProjects.Interface
{
    public interface IDatabaseInterface
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(int customerid);
        Task<IEnumerable<CustomerDTO>> GetCustomerStoredStructure(int customerid = 0);
        Task<IEnumerable<Order>> GetOrders(DateTime? OrderDate = null);
    }
}
