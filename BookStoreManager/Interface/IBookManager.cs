using BookStoreModels;
using Microsoft.Extensions.Configuration;

namespace BookStoreManager.Manager
{
    public interface IBookManager
    {
        IConfiguration Configuration { get; }

        string AddNewBook(BookModel book);
        string DeleteBook(int bookId);
    }
}