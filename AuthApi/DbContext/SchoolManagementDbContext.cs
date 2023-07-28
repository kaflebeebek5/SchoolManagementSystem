

using AuthApi.API.DbModel;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.DbContext
{
    public class SchoolManagementDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
