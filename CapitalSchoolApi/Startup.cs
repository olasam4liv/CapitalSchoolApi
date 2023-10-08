using Microsoft.Azure.Cosmos;

namespace CapitalSchoolApi
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

           
            services.AddControllers();

            services.AddSingleton((provider) =>
            {
                var accountEndpoint = _configuration["CosmosDbSettings:EndpointUrl"];
                var accountKey = _configuration["CosmosDbSettings:PrimaryKey"];
                var databaseName = _configuration["CosmosDbSettings:DatabaseName"];

                var consmosClientOptions = new CosmosClientOptions
                {
                    ApplicationName = databaseName

                };
                var logerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                });

                var cosmosClient = new CosmosClient(accountEndpoint, accountKey, consmosClientOptions);
                cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
                return cosmosClient;
            });



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
