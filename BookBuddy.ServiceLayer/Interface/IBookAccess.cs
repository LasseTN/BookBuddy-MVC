using BookBuddy_MVC.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BookBuddy.ServiceLayer.Interface {
    public interface IBookAccess {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBook(int id);
        Task<(HttpStatusCode, Book?)> CreateBook(Book entity);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}
