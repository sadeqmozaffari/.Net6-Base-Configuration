using App.DataLayer.Mapping;
using App.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DataLayer
{
    public class AppDBContext : IdentityDbContext<User,Role,int,IdentityUserClaim<int>,IdentityUserRole<int>,IdentityUserLogin<int>,RoleClaim,IdentityUserToken<int>>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddCustomIdentityMappings();
            builder.Entity<User>().Property(b => b.RegisterDateTime).HasDefaultValueSql("CONVERT(DATETIME, CONVERT(VARCHAR(20),GetDate(), 120))");
            builder.Entity<User>().Property(b => b.IsActive).HasDefaultValueSql("1");
        }

 
    }
}
