using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Connection;
using WebApplication1.Model.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MyConnectionString")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<MyDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BasePolicy",
    builder =>
    {
        builder
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200", "https://localhost:44336")
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod();
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("BasePolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
