using CapitalSchoolApi;
using Serilog.Events;
using Serilog;

public class Program
{
    static void Main()
    {
        CreateHostBuilder().Build().Run();
    }
    public static IHostBuilder CreateHostBuilder()
    {

        Log.Logger = new LoggerConfiguration()

                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                   .Enrich.FromLogContext()
                   .WriteTo.File(@"C:\AppLogs\CapitalSchoolApi\Log.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    retainedFileCountLimit: 31

                   )
                   .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                   .CreateLogger();


        Log.Information($"The Application is Starting At ....{DateTimeOffset.Now}");
        return Host.CreateDefaultBuilder()
                  .ConfigureWebHostDefaults(webHost =>
                  {
                      webHost.UseStartup<Startup>();
                  });
    }
}