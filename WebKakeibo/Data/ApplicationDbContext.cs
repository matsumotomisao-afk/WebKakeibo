using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebKakeibo.Models;

namespace WebKakeibo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<WebKakeibo.Models.MonthlyBudget> MonthlyBudget { get; set; } = default!;
        public DbSet<WebKakeibo.Models.Payment> Payment { get; set; } = default!;
        public DbSet<WebKakeibo.Models.SubjectName> SubjectName { get; set; } = default!;
    }
}
