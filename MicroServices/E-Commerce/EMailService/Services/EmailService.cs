using Microsoft.EntityFrameworkCore;
using EMailService.Data;
using EMailService.Models;

namespace EMailService.Services
{
    public class EmailService
    {
        private DbContextOptions<AppDbContext> options;

        public EmailService(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }

        public async Task addDatatoDatabase(EmailLogger logger)
        {
            var _db = new AppDbContext(options);
            _db.EmailLoggers.Add(logger);
            await _db.SaveChangesAsync();
        }
    }
}
