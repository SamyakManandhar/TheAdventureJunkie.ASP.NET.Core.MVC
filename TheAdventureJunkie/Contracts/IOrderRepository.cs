using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
    }
}
