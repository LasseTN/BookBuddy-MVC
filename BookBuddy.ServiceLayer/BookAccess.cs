using BookBuddy.ServiceLayer.Interface;
using BookBuddy_MVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace BookBuddy.ServiceLayer {
    public class BookAccess : IBookAccess {
        private readonly ServiceConnection _serviceConnection;
        private readonly ILogger<IBookAccess> _logger;

        public string UseServiceUrl { get; set; }

        public BookAccess(IConfiguration InConfiguration, ServiceConnection serviceConnection, ILogger<IBookAccess> logger) {
            UseServiceUrl = InConfiguration["ServiceUrlToUse"]!;
            _serviceConnection = serviceConnection;
            _serviceConnection.BaseUrl = UseServiceUrl;
            _logger = logger;
        }

        public Task<IEnumerable<Book>> GetAllBooks() {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook(int id) {
            throw new NotImplementedException();
        }

        public async Task<(HttpStatusCode, Book?)> CreateBook(Book entity) {
            Book? createdBook = null;
            HttpStatusCode statusCode = HttpStatusCode.OK;

            _serviceConnection.UseUrl = _serviceConnection.BaseUrl += "Books/";

            if (_serviceConnection != null) {
                try {
                    var json = JsonConvert.SerializeObject(entity);
                    var postData = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage? response = await _serviceConnection.CallServicePost(postData);
                    if (response != null && response.IsSuccessStatusCode) {
                        var content = await response.Content.ReadAsStringAsync();
                        createdBook = JsonConvert.DeserializeObject<Book>(content);
                    } else {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == HttpStatusCode.Conflict) {
                            statusCode = HttpStatusCode.Conflict;
                        } else if (response.StatusCode == HttpStatusCode.InternalServerError) {
                            statusCode = HttpStatusCode.InternalServerError;
                        }
                        Console.WriteLine(responseBody);
                    }
                } catch (Exception ex) {
                    await Console.Out.WriteLineAsync(ex.Message);
                    createdBook = null;
                    _logger.LogInformation("An error occurred while accessing the API: {ErrorMessage}", ex.Message);

                }
            }
            return (statusCode, createdBook);
        }

        public Task UpdateBook(Book book) {
            throw new NotImplementedException();
        }

        public Task DeleteBook(int id) {
            throw new NotImplementedException();
        }


    }
}


