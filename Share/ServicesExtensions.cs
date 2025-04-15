using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Share.Services;

namespace Share
{
    public static class ServicesExtensions
    {
        public static void AddShareServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            //    .MinimumLevel.Error()
            //    .WriteTo.MSSqlServer(
            //        connectionString: configuration.GetConnectionString("LogsDbConnection"),
            //        sinkOptions: new MSSqlServerSinkOptions
            //        {
            //            TableName = "Logs",
            //            AutoCreateSqlDatabase = true
            //        },
            //        columnOptions: GetColumnOptions()
            //    )
            //    .Enrich.FromLogContext()
            //    .CreateLogger();

            //services.AddLogging(logBuilder =>
            //{
            //    logBuilder.AddSerilog();
            //});

        }
        private static ColumnOptions GetColumnOptions()
        {
            var options = new ColumnOptions();

            options.Store.Remove(StandardColumn.Properties);
            options.Store.Add(StandardColumn.LogEvent);
            options.PrimaryKey = options.TimeStamp;

            return options;
        }

    }
}
