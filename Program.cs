using AlarmApp.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<AlarmRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/User/Login";  
		options.LogoutPath = "/User/Logout";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
	});

builder.Services.AddSession(); 
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Alarm}/{action=Index}/{id?}");

app.Run();
