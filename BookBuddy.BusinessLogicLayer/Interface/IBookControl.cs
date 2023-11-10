using BookBuddy_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.BusinessLogicLayer.Interface {
    public interface IBookControl {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
