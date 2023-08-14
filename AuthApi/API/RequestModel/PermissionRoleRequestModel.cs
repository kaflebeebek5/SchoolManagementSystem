namespace AuthApi.API.RequestModel
{
    public class PermissionRoleRequestModel
    {
        public int RoleId { get; set; }
        public int? MenuId { get; set; }
        public List<PermissionRole>? permissionViewModels { get; set; }
    }
    public class PermissionRole
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public int PermissionId { get; set; }

    } 
}
