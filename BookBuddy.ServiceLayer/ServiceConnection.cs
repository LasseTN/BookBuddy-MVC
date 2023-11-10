using BookBuddy.ServiceLayer.Interface;

namespace BookBuddy.ServiceLayer {
    public class ServiceConnection : IServiceConnection {
        public string? BaseUrl { get; set; }
        public string? UseUrl { get; set; }
        public HttpClient HttpEnabler { get; init; }

        public ServiceConnection() {
            HttpEnabler = new HttpClient();
        }

        public async Task<HttpResponseMessage?> CallServiceGet() {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.GetAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(StringContent postJson) {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.PostAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest) {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.SendAsync(postRequest);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePut(StringContent putJson) {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.PutAsync(UseUrl, putJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServiceDelete() {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.DeleteAsync(UseUrl);
            }
            return hrm;
        }
    }
}