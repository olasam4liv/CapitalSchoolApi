using CapitalSchoolApi;

public class Program
{
    static void Main()
    {
        CreateHostBuilder().Build().Run();
    }
    public static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
                  .ConfigureWebHostDefaults(webHost =>
                  {
                      webHost.UseStartup<Startup>();
                  });
    }
}