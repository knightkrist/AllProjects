using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllProjects.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("FirstName", TypeName = "nvarchar(80)")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "nvarchar(80)")]
        public string LastName { get; set; }

        [Column("City", TypeName = "nvarchar(80)")]
        public string City { get; set; }

        [Column("Country", TypeName = "nvarchar(80)")]
        public string Country { get; set; }

        [Column("Phone", TypeName = "nvarchar(40)")]
        public string Phone { get; set; }
    }

    [Table("Order")]
    public class Order
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }
        [Column("OrderNumber", TypeName = "nvarchar(20)")]
        public string OrderNumber { get; set; }
        [Column("CustomerId")]
        public int CustomerId { get; set; }
        [Column("TotalAmount")]
        public decimal? TotalAmount { get; set; }
        [Column("OrderDate")]
        public DateTime OrderDate { get; set; }
    }

    [Table("OrderItem")]
    public class OrderItem
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }
    }
    [Table("Product")]
    public class Product
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("ProductName", TypeName = "nvarchar(100)")]
        public string ProductName { get; set; }

        [Column("SupplierId")]
        public int SupplierId { get; set; }

        [Column("UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Column("Package", TypeName = "nvarchar(60)")]
        public string Package { get; set; }

        [Column("IsDiscontinued")]
        public bool IsDiscontinued { get; set; }
    }
    [Table("Supplier")]
    public class Supplier
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("CompanyName", TypeName = "nvarchar(80)")]
        public string CompanyName { get; set; }

        [Column("ContactName", TypeName = "nvarchar(100)")]
        public string ContactName { get; set; }

        [Column("ContactTitle", TypeName = "nvarchar(80)")]
        public string ContactTitle { get; set; }

        [Column("City", TypeName = "nvarchar(80)")]
        public string City { get; set; }

        [Column("Country", TypeName = "nvarchar(80)")]
        public string Country { get; set; }

        [Column("Phone", TypeName = "nvarchar(60)")]
        public string Phone { get; set; }

        [Column("Fax", TypeName = "nvarchar(60)")]
        public string Fax { get; set; }
    }
}
