using Exam.Application.Interfaces;
using Exam.Application.Services;
using Exam.Infraestructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Exam
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ExamContext>();

                    context.Database.Migrate();

                    if (context.Users.Any() == false)
                    {
                        var userDownloaderService = services.GetRequiredService<IUserDownloaderService>();

                        userDownloaderService.DownloadAndSaveUsersFromEndPoint("https://randomuser.me/api/?results=500");
                    }                
                }
                catch (Exception ex)
                {
                   
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
