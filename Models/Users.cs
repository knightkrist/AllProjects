using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllProjects.Models
{
    [Table("Users")]
    public class Users
    {
        [Column("UserID")]
        [Key]
        public int UserID { get; set; }

        [Column("GUID", TypeName = "varchar(50)")]
        public string GUID { get; set; }

        [Column("UserName", TypeName = "varchar(500)")]
        public string UserName { get; set; }

        [Column("CustomerID")]
        public int? CustomerID { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
    }
}
