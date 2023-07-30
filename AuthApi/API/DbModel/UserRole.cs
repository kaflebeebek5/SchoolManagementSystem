using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.API.DbModel
{
    [Table("tblUserRole", Schema = "Auth")]
    public class UserRole
    {
        [Key]
        public int ID { get; set; }
        public int RoleId { get; set; }
        public int UserID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
