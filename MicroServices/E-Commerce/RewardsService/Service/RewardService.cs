using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RewardsService.Models;
using RewardsService.Service.IService;
using RewardsService.Data;

namespace RewardsService.Service
{
    public class RewardService : IReward

    {
        private DbContextOptions<AppDbContext> options;

        public RewardService(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }
        public async Task AddReward(Reward reward)
        {
            var _db = new AppDbContext(options);
            _db.Rewards.Add(reward);
            await _db.SaveChangesAsync();
        }
    }
}
