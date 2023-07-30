namespace AuthApi.Authentication.Model
{
    public class UserClaimModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
