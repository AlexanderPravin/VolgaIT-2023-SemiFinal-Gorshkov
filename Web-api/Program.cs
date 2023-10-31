using Infrastructure.VolgaIT.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using App.VolgaIT;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.VolgaIT;
using Domain.VolgaIT.Settings;
using Domain.VolgaIT.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JwtOptions>(options =>
    builder.Configuration.GetSection("JwtOptions")
    );

builder.Services.Configure<PasswordHashOptions>(options =>
    builder.Configuration.GetSection("PasswordHashOptions")
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "VolgaIT", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insert Json Web Token",
            Name = "Swagger autorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string> ()
            }
        });
    }
    );

builder.Services.AddDbContext<VolgaContext>
    (options => options
    .UseNpgsql(builder.Configuration
    .GetConnectionString("PSQSQL"))
    );

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "TestIssuer",
            ValidateIssuer = true,

            ValidateLifetime = true,

            ValidateAudience = false,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("asdfghjkl123456789qwe")),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddRepository();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
