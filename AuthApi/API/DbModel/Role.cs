using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.API.DbModel
{
    [Table("tblRole", Schema = "Auth")]
    public class Role:MasterEntity
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public int Status { get; set; }
    }
}
