using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.API.DbModel
{
    [Table("tblPermissionRole", Schema = "Auth")]
    public class PermissionRole
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
