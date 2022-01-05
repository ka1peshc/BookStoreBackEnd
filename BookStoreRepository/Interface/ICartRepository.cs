using BookStoreModels;

namespace BookStoreRepository.Repository
{
    public interface ICartRepository
    {
        string connectionString { get; set; }

        string AddNewItemInCart(CartModel item);
    }
}