using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using LibOwin;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using SimpleTodoList.Infrastracture;
using Microsoft.EntityFrameworkCore;

namespace SimpleTodoList
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["Data::TodoListDb:ConnectionString"];
            //    Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            //Trace.WriteLine(connection);
            //Configuration.GetConnectionString("TodoListConnStr")
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<SimpleTodoListDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(buildFunc =>
            {
                //buildFunc(next => env =>
                //{
                //    var ctx = new OwinContext(env);
                //    var principal = ctx.Request.User;
                //    if (principal?.HasClaim("scope", "simple_todolist_writer") ?? false)
                //        return next(env);
                //    ctx.Response.StatusCode = 403;
                //    return Task.FromResult(0);
                //});
                buildFunc(next => env =>
                {
                    var ctx = new OwinContext(env);
                    var idToken = ctx.Request.User?.FindFirst("id_token");
                    if (idToken != null)
                    {
                        ctx.Set("pos-end-user-token", idToken);
                    }
                    return next(env);
                });
                buildFunc(next => env =>
                {
                    var ctx = new OwinContext(env);
                    if (ctx.Request.Headers.ContainsKey("pos-end-user"))
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        SecurityToken token;
                        var userPrincipal =
                          tokenHandler.ValidateToken(ctx.Request.Headers["pos-end-user"],
                                                     new TokenValidationParameters(),
                                                     out token);
                        ctx.Set("pos-end-user", userPrincipal);
                    }
                    return next(env);
                });
                buildFunc.UseNancy();
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();

            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            
        }
    }
}
