using Asp.Versioning;
using Demo1.Data;
using Demo1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IDemoRepository, DemoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
})
.AddMvc()
.AddApiExplorer(options =>
{
    // THis replaces the v{version} in swagger page for endpoint urls
    options.GroupNameFormat = "VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    // Add swagger pages for different versions
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Demo1 API v1 Vesrions test",
        Version = "v1"
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Demo1 API v2 versions",
        Version = "v2"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Add dropdown options
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Version v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "Version v2");
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
