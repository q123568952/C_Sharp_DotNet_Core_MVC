using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Dropcat.Data;
using Dropcat.Models;
using Dropcat.Models.Dao;
using Dropcat.Models.Dao.Impl;
using Dropcat.Models.Service.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
}
);

//like 指定bean
builder.Services.AddScoped(typeof(UserDao), typeof(UserDaoImpl));
builder.Services.AddScoped(typeof(UserService), typeof(UserServiceImpl));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
