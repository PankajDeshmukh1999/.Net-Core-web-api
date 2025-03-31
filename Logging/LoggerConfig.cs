using Serilog;

namespace StudentManagement.Logging

{
    public class LoggerConfig
    {
        public static void ConfigureLogger(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/student-service-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
