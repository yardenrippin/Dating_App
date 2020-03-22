using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using My_App.Data;

namespace My_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
         var host= CreateWebHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    seed.Sedduser(context);
                }
                catch (Exception ex)
                {
                    var looger = service.GetRequiredService<ILogger<Program>>();
                    looger.LogError(ex, "an error occured during migration");
                   
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
