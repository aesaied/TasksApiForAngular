
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TasksApi.Entities;

namespace TasksApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            builder.Services.AddDbContext<AppDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskAPIDB"));
            
            });

            builder.Services.AddIdentity<AppUser,AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var audience = builder.Configuration.GetValue<string>("JWT:Audiance");
            var issuer = builder.Configuration.GetValue<string>("JWT:Issuer");
            var SignKey = builder.Configuration.GetValue<string>("JWT:SignKey");

            SignKey ??= "";



            builder.Services.AddAuthentication(options=>
            {
                
                options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            
            }).AddJwtBearer(options => {

                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SignKey))
                };
            }
                
                );

            builder.Services.AddCors(options =>
            {


                options.AddPolicy("MyPolicy", policy =>
                {
                    //
                    policy.WithOrigins(builder.Configuration.GetValue<string>("FrontEndApp")).AllowAnyMethod().AllowAnyHeader();
                });

            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

 
            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");
            app.UseAuthentication();

            // custom authentication
            app.Use(async (cntx, next) => {

                if (cntx != null)
                {
                    if (cntx.User.Identity.IsAuthenticated == false)
                    {
                        //  Authenticate  by  force

                        if (cntx.Request.Headers.ContainsKey("Authorization"))
                        {

                         var  result = await  cntx.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                        }
                    }

                }

               await next?.Invoke();
            
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
