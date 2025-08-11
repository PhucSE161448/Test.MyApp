using Test.MyApp.Web.Configuration;
using Test.MyApp.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.SettingsBinding();
builder.Services.AddSwaggerGenOption();
builder.Services.AddJwtValidation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext();
//Config builder
builder.ConfigureAutofacContainer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(p =>
    {
        p.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
