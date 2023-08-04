
using Asp.Versioning;
using Conference.Data;
using Conference.Domain.Entities;
using Conference.Domain.Extensions;
using Conference.Service;
using ConferenceApi.Infrastructure.Constraints;
using ConferenceApi.Infrastructure.Middleware.Extensions;
using ConferenceApi.Infrastructure.OpenAPi;
using ConferenceApi.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // options.ValueProviderFactories.Add(new CommaQueryStringValueProviderFactory());
    //options.ReturnHttpNotAcceptable = true;
    //options.RespectBrowserAcceptHeader = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ConferenceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConferenceDatabase"),
        m => m.MigrationsAssembly("Conference.Domain"));
});

builder.Services.AddScoped<ISpeakersRepository, SpeakersRepository>();
builder.Services.AddScoped<ISpeakersService, SpeakersService>();

builder.Services.AddScoped<ITalksRepository, TalksRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(SpeakerProfile));

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("email", typeof(EmailRouteConstraint));
});

builder.Services.AddApiVersioning(options =>
{
})
    .AddApiExplorer(o =>
    {
        o.GroupNameFormat = "'v'VVV";
        o.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

var app = builder.Build();
//var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())//apiVersionDescriptionProvider.ApiVersionDescriptions
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }

        //options.RoutePrefix = string.Empty;
        //options.SwaggerEndpoint("");
    });
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider?.GetService<ConferenceContext>()?.Database.EnsureCreated();
    serviceScope.ServiceProvider?.GetService<ConferenceContext>()?.EnsureSeeded();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSecurityHeaders();

app.MapControllers();

app.Run();
