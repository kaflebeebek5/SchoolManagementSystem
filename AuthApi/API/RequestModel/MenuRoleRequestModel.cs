namespace AuthApi.API.RequestModel
{
    public class MenuRoleRequestModel
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public List<MenuRole>? menuRoles { get; set; }
    }
    public class MenuRole
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public int MenuId { get; set; }
        public int? ParentId { get; set; }
    }
    public class MenuRoleResponseModel
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int? ParentId { get; set; }
        public string? ParentMenu { get; set; }
        public string? MenuName { get; set; }
    }
    public class UserRoleResponseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PermissionRoleResponseModel
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public int RoleId { get; set; }
        public int? MenuId { get; set; }
        public int PermissionId { get; set; }
        public string? PermissionValue { get; set; }
        public string? MenuName { get; set; }
    }
}
