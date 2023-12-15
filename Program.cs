using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebLibary.Models.Entities;
using WebLibary.Repository;
using WebLibary.Repository.ERepository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibaryContext>
    (option => option.UseSqlServer
    (builder.Configuration.GetConnectionString("ConnectStr")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(TRepository<>));

builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));


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
