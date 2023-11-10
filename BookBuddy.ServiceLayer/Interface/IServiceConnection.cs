namespace BookBuddy.ServiceLayer.Interface {
    public interface IServiceConnection {
        string? BaseUrl { get; set; }
        string? UseUrl { get; set; }
        HttpClient HttpEnabler { get; }

        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest); // Changed to HttpRequestMessage
        Task<HttpResponseMessage?> CallServicePut(StringContent putJson);
        Task<HttpResponseMessage?> CallServiceDelete();
    }
}
