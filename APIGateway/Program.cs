var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AuthApiBaseUrl"]);
});
//services.AddHttpClient("InventoryApi", client =>
//{
//    client.BaseAddress = new Uri(_configuration["InventoryApiBaseUrl"]);
//});
//services.AddHttpClient("MasterSetupApi", client =>
//{
//    client.BaseAddress = new Uri(_configuration["MasterSetupApiBaseUrl"]);
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/api", app =>
{
    app.Run(async context =>
    {
        var path = context.Request.Path;

        if (path.StartsWithSegments("/Auth"))
        {
            await ProxyRequestAsync(context, "AuthApi");
        }
        else if (path.StartsWithSegments("/inventory"))
        {
            await ProxyRequestAsync(context, "InventoryApi");
        }
        else if (path.StartsWithSegments("/mastersetup"))
        {
            await ProxyRequestAsync(context, "MasterSetupApi");
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
    });
});
app.Run();
async Task ProxyRequestAsync(HttpContext context, string httpClientName)
{
    try
    {
        var httpClientFactory = context.RequestServices.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(httpClientName);

        var requestMessage = new HttpRequestMessage
        {
            Method = new HttpMethod(context.Request.Method),
            RequestUri = new Uri(httpClient.BaseAddress + context.Request.Path),
            Headers = { /* Copy headers from the original request as needed */ }
        };

        using (var response = await httpClient.SendAsync(requestMessage))
        {
            context.Response.StatusCode = (int)response.StatusCode;
            foreach (var header in response.Headers)
            {
                context.Response.Headers.Add(header.Key, header.Value.ToArray());
            }

            var contentStream = await response.Content.ReadAsStreamAsync();
            await contentStream.CopyToAsync(context.Response.Body);
        }
    }
    catch (Exception ex)
    {

        throw ex;
    }
}

await app.RunAsync();

