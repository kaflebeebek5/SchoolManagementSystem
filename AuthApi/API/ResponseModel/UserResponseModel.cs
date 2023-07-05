namespace AuthApi.API.ResponseModel
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? JoinDate { get; set; }
        public string? ValidFrom { get; set; }
        public string? ValidTo { get; set; }
        public int Lock { get; set; }
        public string? Photo { get; set; }
        public int Status { get; set; }
    }
}
