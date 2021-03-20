using System;
using AppGmz.Models.DomainModels;
using AppGmz.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppGmz.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public DbSet<RecordNews> RecordNewses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
    }
}