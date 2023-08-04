using Conference.Data;
using Conference.Domain.Entities;
using Conference.Domain.Extensions;
using Conference.Service;
using ConferenceApi.Infrastructure.Constraints;
using ConferenceApi.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.ReturnHttpNotAcceptable = true;
//    options.RespectBrowserAcceptHeader = true;
//});

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider?.GetService<ConferenceContext>()?.Database.EnsureCreated();
    serviceScope.ServiceProvider?.GetService<ConferenceContext>()?.EnsureSeeded();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "internal route",
//    pattern: "api/internal/{controller=Values}/{action}");

//api/myvalues/getall
//app.MapControllerRoute(
//    name: "internal route prepended",
//    pattern: "api/My{controller=Values}/{action}");

// api / myvaluess / getall
//api / myvalues / getall
//app.MapControllerRoute(
//    name: "internal route appended",
//    pattern: "api/{controller=Values}S/{action}");


app.Run();
