using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.API.DbModel
{
    [Table("tblPermission", Schema = "Auth")]
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public string? PermissionType { get; set; }
        public string? PermissionValue { get; set; }
        public string? PermissionDescription { get; set; }
        public int MenuId { get; set; }
        
    }
}
