using Asp.Versioning;
using Microsoft.OpenApi.Models;
using MiniProject4.Application.Interfaces;
using MiniProject4.Application.Services;
using MiniProject4.Domain.Interfaces;
using MiniProject4.Infrastructure;
using MiniProject4.Infrastructure.Data.Repositories;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add cors for allow front end
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5173") // Replace with your React app's URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.
builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
    option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
    option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.

    //------------------------------------------------//
    option.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")); //This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
                                               //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.
});
//Swagger Documentation Section
var info = new OpenApiInfo()
{
    Title = "Your API Documentation",
    Version = "v1",
    Description = "Description of your API",
    Contact = new OpenApiContact()
    {
        Name = "Your name",
        Email = "your@email.com",
    }

};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(u =>
    {
        u.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Your API Title or Version");
    });
}

// Add cors for allow front end
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
