using System;
using Coinquarry.Client;
using Coinquarry.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Coinquarry.Host;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    private readonly ILoggerFactory _loggerFactory;
    
    
    public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        Configuration = configuration;
        _loggerFactory = loggerFactory;
    }
    

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        ConfigureSwagger(services);
        
        Type typeOfContent = typeof(Startup);
        services.AddDbContext<PostgreSqlContext>(
            opt => opt.UseNpgsql(
                Configuration.GetConnectionString("PostgreSqlConnection"),
                b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
            )
        );
        
        services.AddScoped<IDatabaseContainer, DatabaseContainer>();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coinquarry.Host v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }


    public void ConfigureSwagger(IServiceCollection services)
    {
        string GetSchemaId(Type t)
        {
            var messagesRootNamespace = typeof(AbstractResponse).Namespace;
            var messagesStartPrefix = messagesRootNamespace + '.';
        
            var fullname = t.FullName;
        
            if (fullname.StartsWith(messagesStartPrefix) == false)
            {
                var value = typeof(IFormFile).Namespace;
                if (value != null && fullname.StartsWith(value) == false)
                {
                    throw new Exception($"Messages must be in {messagesRootNamespace} namespace, but this message is not: {fullname}");
                }
            }
        
            var prepared = fullname.Substring(messagesStartPrefix.Length).Replace('+', 'R');
            return prepared;
        }

        services.AddSwaggerGen(
            c =>
            {
                c.CustomSchemaIds(GetSchemaId);
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Coinquarry.Host", Version = "v1"});
            }
        );
    }
}