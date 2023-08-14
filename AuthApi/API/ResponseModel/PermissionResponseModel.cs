namespace AuthApi.API.ResponseModel
{
    public class PermissionResponseModel
    {
        public int PermissionId { get; set; }
        public string? PermissionType { get; set; }
        public string? PermissionValue { get; set; }
        public string? PermissionDescription { get; set; }
        public int MenuId { get; set; }
    }
}
