using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IBookRepository
    {
        string connectionString { get; set; }

        string AddNewBook(BookModel model);
        string DeleteBook(int bookId);
        string UpdateBook(BookModel model);
        IEnumerable<BookModel> DisplayOneBook(int bookId);
        IEnumerable<BookModel> DisplayAllBooks();
    }
}