using Conference.Api.Infrastructure.ValueProviders;
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
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
   // options.ValueProviderFactories.Add(new CommaQueryStringValueProviderFactory());
    //options.ReturnHttpNotAcceptable = true;
    //options.RespectBrowserAcceptHeader = true;
});


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



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddApiVersioning(options =>
{
})
.AddApiExplorer(o =>
{

    o.GroupNameFormat = "'v'VVV";

});

builder.Services.AddSwaggerGen();

var app = builder.Build();

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
