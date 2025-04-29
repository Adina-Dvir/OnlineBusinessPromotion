//using 
using Microsoft.OpenApi.Models;
using Mock;
using Repository.Interfaces;
using Repository.Entities;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using Common.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
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

// Dependency Injection

// Professional Service
builder.Services.AddScoped<IService<ProfessionalsDto>, ProfessionalService>();

// Professional Repository
builder.Services.AddScoped<IRepository<Professionals>, ProfessionalsRepository>();
//UserService
builder.Services.AddScoped<IService<UserDto>, UserService>();
//UserRepository
builder.Services.AddScoped<IRepository<User>, UserRepository>();
//CategoryService
builder.Services.AddScoped<IService<CategoryDto>, CategoryService>();

//CategoryRepositotry
builder.Services.AddScoped<IRepository<Category>, CategoryRepositotry>();
//IContext
builder.Services.AddScoped<IContext, Database>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MyMapper));

// DbContext
builder.Services.AddDbContext<IContext, Database>();

// CORS
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

// Authorization Middleware
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();