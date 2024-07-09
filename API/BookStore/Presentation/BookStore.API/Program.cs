


using BookStore.Application.Commands.Categorys;
using BookStore.Application.Interfaces;
using BookStore.Application.Mapping;
using BookStore.Application.Services;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Interfaces;
using BookStore.Infrastructure.Repositories;
using BookStore.Presentation.Extensions;
using BookStore.Presentation.middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;


var builder = WebApplication.CreateBuilder(args);
// NLog configuration
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Configure Swagger to include Bearer token input
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token in the following format: {your token here} do not add the word 'Bearer' before it."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext< BookStoreContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
        b => b.MigrationsAssembly("BookStore.Infrastructure"));
});
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining(typeof(CreateCategory)));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

// Add NSwag services
builder.Services.AddOpenApiDocument();


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = " Book Store Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Store Demon API");
});


// Register the custom middleware
app.UseMiddleware<CustomUnauthorizedResponseMiddleware>();
app.MapControllers();

app.Run();
