using EMailService.Models;
using Microsoft.EntityFrameworkCore;

namespace EMailService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public  DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
