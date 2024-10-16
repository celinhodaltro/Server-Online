using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Entities;
using System.Provider;
using System.Text;
using Server.Util;
using Server.BusinessRules;
using Server.Providers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();




        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                )
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                });
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL("Server=localhost;Database=AppMain;Uid=root;Pwd=admin"));

        InjectDefaultServices(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();
        app.UseCors();
        app.MapControllers();

        app.Run();
    }




    public static void InjectDefaultServices( IServiceCollection Services)
    {
        InjecterDefault(Services);
        InjecterBusinessRules(Services);
        InjecterProvider(Services);
    }

    public static void InjecterDefault(IServiceCollection Services)
    {
        Services.AddScoped<ApplicationDbContext>();
    }

    public static void InjecterBusinessRules(IServiceCollection Services)
    {
        Services.AddScoped<LoggerBusinessRules>();
        Services.AddScoped<UserBusinessRules>();
        Services.AddScoped<CharacterBusinessRules>();

    }

    public static void InjecterProvider(IServiceCollection Services)
    {
        Services.AddScoped<DefaultProvider>();
        Services.AddScoped<UserProvider>();
        Services.AddScoped<CharacterProvider>();
    }

    
}