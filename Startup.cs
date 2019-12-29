using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsService.DataLayer;

namespace ProductsService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<StackdriverOptions>(

                Configuration.GetSection("Stackdriver"));

            services.AddGoogleExceptionLogging(options =>

            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.ServiceName = Configuration["Stackdriver:ServiceName"];
                options.Version = Configuration["Stackdriver:Version"];
            });

            // Add trace service.
            services.AddGoogleTrace(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //register the DBContext in in-memory with ProductService application. 
            services.AddDbContext<DataContext>(opt =>
               opt.UseInMemoryDatabase("ProductList"));

            ////Database communication
            //services.AddDbContext<DataContext>(x =>
            //x.UseSqlServer(Configuration.GetConnectionString("ProductInventory")));

            //Enable CORS! Create a middleware
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Configure error reporting service.

            app.UseGoogleExceptionLogging();

            // Configure trace service.
  
            // Configure error reporting service.
            app.UseGoogleExceptionLogging();
            // Configure trace service.
            app.UseGoogleTrace();
            app.UseStaticFiles();



            app.UseCors(policyName: "EnableCORS");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
