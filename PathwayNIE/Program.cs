using Microsoft.EntityFrameworkCore;
using PathwayNIE.Models.Entities;
using System.Collections.Specialized;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AspNetCore.Unobtrusive.Ajax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("Context_PosgreSQL_Local");

connection = builder.Configuration.GetConnectionString("Context_PosgreSQL_NotLocal");

// Подключение по PosgreSql
builder.Services.AddDbContext<PathwayContext>(options => options.UseNpgsql(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

// аутентификация с помощью куки
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();   // добавление middleware аутентификации 
app.UseAuthorization();   // добавление middleware авторизации 

//app.Map("/hello", [Authorize] () => "Hello World!");
//app.Map("/", () => "Home Page");

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
	pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
