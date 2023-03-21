using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllProjects.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public List<OrderDTO> orders { get; set; }
    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public CustomerDTO customerDTO { get; set; }
        public List<OrderItemDTO> orderItems { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ProductDTO productDTO { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public SupplierDTO supplierDTO { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }

    public class SupplierDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
