namespace Gateway.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;
    using System.Collections.Generic;

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
            services.AddOcelot();
            services.AddSwaggerForOcelot(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            app.UsePathBase("/gateway");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                opt.PathToSwaggerGenerator = "/swagger/docs";
                opt.ReConfigureUpstreamSwaggerJson = AlterUpstreamSwaggerJson;
                opt.ServerOcelot = "localhost";
                opt.DownstreamSwaggerHeaders = new[]
                  {
                      new KeyValuePair<string, string>("Auth-Key", "AuthValue"),
                  };
            }).UseOcelot();

            //logger.AddFile("LogsSerilog/log_services-{Date}.txt");
        }

        public string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
        {
            var swagger = JObject.Parse(swaggerJson);
            // ... alter upstream json
            return swagger.ToString(Formatting.Indented);
        }
    }
}
