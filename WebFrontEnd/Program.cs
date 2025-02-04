using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string? httpClientName = builder.Configuration["MyWebApiHttpClientName"];
ArgumentException.ThrowIfNullOrEmpty(httpClientName);
builder.Services.AddHttpClient(httpClientName, 
        client =>
        {
            string hostname = builder.Configuration.GetValue<bool>("IsContainerized")
                ? "mywebapi:8080"
                : "localhost:5178";

            client.BaseAddress = new Uri($"http://{hostname}/");
        })
    .UseSocketsHttpHandler((handler, _) => 
        handler.PooledConnectionLifetime = TimeSpan.FromMinutes(20))
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan); // Disable rotation, as it is handled by PooledConnectionLifetime

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
