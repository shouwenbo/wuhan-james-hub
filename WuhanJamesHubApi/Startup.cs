using Newtonsoft.Json;

namespace WuhanJamesHubApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DirectoryService>();
            services.AddSingleton<JJBService>();

            services.AddMemoryCache();
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            //services.AddCors();

            // Ìí¼Ó CORS ·þÎñ
            services.AddCors(options =>
            {
                options.AddPolicy("myCors",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddControllers(options =>
            {
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DirectoryService directoryService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();

            app.UseCors("myCors");

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}