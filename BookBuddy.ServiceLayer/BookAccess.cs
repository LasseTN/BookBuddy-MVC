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

        public async Task<IEnumerable<Book>> GetAllBooks() {
            List<Book> foundBooks = new List<Book>();
            var fullUrl = $"{_serviceConnection.BaseUrl.TrimEnd('/')}/Book"; // Ensure this matches the API route

            if (_serviceConnection != null) {
                try {
                    _serviceConnection.UseUrl = fullUrl;
                    var response = await _serviceConnection.CallServiceGet();
                    if (response != null && response.IsSuccessStatusCode) {
                        var content = await response.Content.ReadAsStringAsync();
                        foundBooks = JsonConvert.DeserializeObject<List<Book>>(content);
                    } else {
                        _logger.LogError($"Failed to retrieve books. Status code: {response.StatusCode}");
                    }
                } catch (Exception ex) {
                    _logger.LogError($"An error occurred while fetching books: {ex.Message}");
                }
            }
            return foundBooks;
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


