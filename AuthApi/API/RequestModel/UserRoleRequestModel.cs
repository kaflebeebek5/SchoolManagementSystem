namespace AuthApi.API.RequestModel
{
    public class UserRoleRequestModel
    {
        public int? UserId { get; set; }
        public List<UserRole>? userRoles { get; set; }
    }
    public class UserRole
    {
        public int? RoleId { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsChecked { get; set; }
    }
}
