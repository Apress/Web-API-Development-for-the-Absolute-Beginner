var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();
//app.Map("/branch1", HandleFirstBranch);
//app.Map("/branch2", HandleSecondBranch);
//app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleThirdBranch);
//app.UseWhen(context => context.Request.Query.ContainsKey("otherBranch"),
//    appBuilder => HandleBranchAndRejoin(appBuilder));

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("custom-header", "custom-value");
//    // Do work that can write to the Response.
//    await next.Invoke();
//    // Do logging or other work that doesn't write to the Response.

//});
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from the last delegate.");
//});
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("I shouldn't be executed.");
//});


//used to match the routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Values}/{action=Get}/{id?}",
    constraints: null,
    defaults: null);

//app.MapControllerRoute(name: "blog",
//                pattern: "blog/{*article}",
//                defaults: new { controller = "Blog", action = "Article" });

//app.MapControllers();

static void HandleFirstBranch(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map - First Branch");
    });
}

static void HandleSecondBranch(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map - Second Branch");
    });
}
static void HandleThirdBranch(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        var branchNumber = context.Request.Query["branch"];
        await context.Response.WriteAsync($"Map - branch no: {branchNumber}");
    });
}

void HandleBranchAndRejoin(IApplicationBuilder app)
{
    var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();

    app.Use(async (context, next) =>
    {
        var branchNumber = context.Request.Query["otherBranch"];
        logger.LogInformation($"Map - otherBranch no: {branchNumber}");

        // Do work that doesn't write to the Response.
        await next();
        // Do some other work that doesn't write to the Response.
    });
}

app.Run();
