using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IBookManager
    {
        IConfiguration Configuration { get; }

        string AddNewBook(BookModel book);
        string DeleteBook(int bookId);
        string UpdateBook(BookModel book);
        IEnumerable<BookModel> DisplayOneBook(int bookId);
        IEnumerable<BookModel> DisplayAllBooks();
    }
}