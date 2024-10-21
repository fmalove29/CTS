using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CrudAsp.Models.Data;
using CrudAsp.Models;
using CrudAsp.Models.app;
using CrudAsp.Services.Movies;
using CrudAsp.Services.Genres;
using CrudAsp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddScoped<IRepository<Users>, Repository<Users>>();
// builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();
builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped
    );

// Configure Identity
builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();
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

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
