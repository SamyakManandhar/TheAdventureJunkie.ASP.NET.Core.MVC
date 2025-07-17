using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
