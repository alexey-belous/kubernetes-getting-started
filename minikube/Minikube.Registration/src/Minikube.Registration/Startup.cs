using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minikube.Registration.Api.Registration;
using Minikube.Registration.Configuration;
using MongoDB.Driver;

namespace Minikube.Registration
{
    public class Startup
    {
        public static Guid InstanceId = Guid.NewGuid();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(StorageConfig.Load(Configuration));
            services.AddTransient(typeof(MongoClient),
                sp => new MongoClient(connectionString: sp.GetService<StorageConfig>().ConnectionString));

            services.AddTransient<IUserWriter, UserStorage>();
            services.AddTransient<IUserFinder, UserStorage>();
            services.AddTransient<IUserRegisterer, UserRegisterer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
