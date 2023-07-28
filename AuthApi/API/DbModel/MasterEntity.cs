namespace AuthApi.API.DbModel
{
    public class MasterEntity
    {
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
