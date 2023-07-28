namespace AuthApi.API.RequestModel
{
    public class UserReuestModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int Lock { get; set; }
        public string? Photo { get; set; }
        public int Status { get; set; }

    }
}
