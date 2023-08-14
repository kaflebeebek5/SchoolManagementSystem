using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.API.DbModel
{
    [Table("tblMenu", Schema = "Auth")]
    public class Menu:MasterEntity
    {
        [Key] public int MenuID { get; set; }
        public string? MenuName { get; set; }
        public string? MenuLabel { get; set; }
        public string? FormName { get; set; }
        public string? ShortCutKey { get; set; }
        public string? URL { get; set; }
        public string? MenuIcon { get; set; }
        public int? ParentID { get; set; }
        public int? MenuOrder { get; set; }
        public bool Status { get; set; }
    }
}
