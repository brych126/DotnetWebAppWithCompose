using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            string? httpClientName = configuration["MyWebApiHttpClientName"];
            ArgumentException.ThrowIfNullOrEmpty(httpClientName);
            _httpClient = httpClientFactory.CreateClient(httpClientName);
        }

        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync("Counter");
            string counter = await response.Content.ReadAsStringAsync();
            ViewData["Message"] = $"Counter value from cache :{counter}";
        }
    }
}
