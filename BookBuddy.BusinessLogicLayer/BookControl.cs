using BookBuddy.BusinessLogicLayer.Interface;
using BookBuddy.ServiceLayer.Interface;
using BookBuddy_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookBuddy.BusinessLogicLayer {
    public class BookControl : IBookControl {

        private readonly IBookAccess _bookAccess;

        public BookControl(IBookAccess bookAccess) {
            _bookAccess = bookAccess;

        }
        public Task CreateBook(Book book) {
            return _bookAccess.CreateBook(book);
        }

        public Task<IEnumerable<Book>> GetAllBooks() {
            return _bookAccess.GetAllBooks();
        }

        public Task<Book> GetBook(int id) {
            return _bookAccess.GetBook(id);
        }

        public Task UpdateBook(Book book) {
            return _bookAccess.UpdateBook(book);
        }

        public Task DeleteBook(int id) {
            return _bookAccess.DeleteBook(id);
        }
    }
}
