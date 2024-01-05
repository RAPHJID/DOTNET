using RewardsService.Models;

namespace RewardsService.Service.IService
{
    public interface IReward
    {
        Task AddReward(Reward reward);
    }
}
