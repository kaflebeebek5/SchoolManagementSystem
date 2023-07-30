

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
        public DbSet<UserRole>  userRoles { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
