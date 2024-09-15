using MiddlewareSample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure authentication and authorization
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "SimpleAuthCookie";
        options.LoginPath = "/api/auth";
    });
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// End configure authentication and authorization

// Simple custom middleware from class
//app.UseMiddleware<SimpleMiddleware>();

// Exception handling middleware
//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.MapGet("/", () => { throw new ApplicationException("Sample exception to handle"); });

// Logging middleware
//app.UseMiddleware<RequestLoggingMiddleware>();
//app.MapGet("/", () => "Hello World!");

// Chaining multiple middlewares
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello from 1st middleware\n");
//    await next.Invoke();
//});
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello from 2nd middleware\n");
//    await next.Invoke();
//});

// Multiple terminal middlewares registered, only the first one will be executed
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from 1st terminal middleware\n");
//});
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from 2nd terminal middleware\n");
//});

app.Run();
