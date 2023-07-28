using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.API.DbModel
{
    [Table("tblUser", Schema = "Auth")]
    public class User:MasterEntity
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Photo { get; set; }
        public int Status { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public string? MobileNo { get; set; }
        public int IsLocked { get; set; }
    }
}
