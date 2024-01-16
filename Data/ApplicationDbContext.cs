using CamXucWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CamXucWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Review>? reviews { get; set; }
    }
}
