using Microsoft.OpenApi.Models;
using Repository.Interfaces;
using Repository.Entities;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mock;
using Service.Logic;
using Nest;
using Repository.Entities.Entities;
using Service.Services;  // או המקום שבו מוגדרת המחלקה שלך
using Service.Mapping;


var builder = WebApplication.CreateBuilder(args);

// ---------- Load configuration ----------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

// ---------- Swagger ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT with Bearer format",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

// ---------- Services & DI ----------
builder.Services.AddScoped<IContext, Database>();
builder.Services.AddDbContext<IContext, Database>();
builder.Services.AddScoped<IClickRepository, ClickRepository>();
builder.Services.AddRepository();
builder.Services.AddServices();
builder.Services.AddScoped<TrendingService>();
builder.Services.AddScoped<RankingService>();
builder.Services.AddScoped<ClickSeeder>();
builder.Services.AddScoped<BusinessRankingExecutor>();
builder.Services.AddControllers();

// ---------- CORS ----------
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// ---------- JWT Authentication ----------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ✅ הוספת AutoMapper כאן, לפני Build
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// ✅ Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

//----- הוספת הקריאה לסידר:
//using (var scope = app.Services.CreateScope())
//{
//    var seeder = scope.ServiceProvider.GetRequiredService<ClickSeeder>();
//    await seeder.SeedClicksAsync();
//}

// ---------- Middleware Pipeline ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

Console.WriteLine("JWT KEY => " + builder.Configuration["Jwt:Key"]);

app.UseAuthorization();

app.MapControllers();

app.Run();
