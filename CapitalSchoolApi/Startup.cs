using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Services;
using Microsoft.Azure.Cosmos;
using Serilog;
using Serilog.Events;
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

            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IWorkflowService, WorkflowService>();

            services.AddSingleton((provider) =>
            {
                var accountEndpoint = _configuration["CosmosDbSettings:EndpointUrl"];
                var accountKey = _configuration["CosmosDbSettings:PrimaryKey"];
                var databaseName = _configuration["CosmosDbSettings:DatabaseName"];

                var consmosClientOptions = new CosmosClientOptions
                {
                    ApplicationName = databaseName

                };
                //var logerFactory = LoggerFactory.Create(builder =>
                //{
                //    builder.AddConsole();
                //});

                

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
