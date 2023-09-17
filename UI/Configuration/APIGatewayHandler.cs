namespace UI.Configuration
{
    public static class APIGatewayHandler
    {
        public static IServiceCollection AddApiHandlers(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddHttpClient("ApiGateway", client =>
            {
                client.BaseAddress = new Uri(_configuration.GetSection("ApiUrl:ApiGateway").Value!);
            });
            return services;
        }
    }
}
