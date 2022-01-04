using BookStoreModels;

namespace BookStoreRepository.Repository
{
    public interface IBookRepository
    {
        string connectionString { get; set; }

        string AddNewBook(BookModel model);
        string DeleteBook(int bookId);
    }
}