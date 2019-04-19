using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Exam.Application.Interfaces;
using Exam.Application.IoC.Modules;
using Exam.Application.Mapping.Profiles;
using Exam.Application.Services;
using Exam.Infraestructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Exam
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Autofac container
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var builder = new ContainerBuilder();

            services.AddDbContext<ExamContext>(options => options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            //register here for use it on database seed.
            services.AddTransient<IUserDownloaderService, UserDownloaderService>();            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            builder.Populate(services);
            builder.RegisterModule(new DataAccessModule());
            builder.RegisterModule(new ApplicationModule());
            
            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingEntityToDto());
            });

            app.UseMvc();

        }
    }
}
