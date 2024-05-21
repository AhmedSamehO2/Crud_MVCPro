using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sneat.BLL.Interfaces;
using Sneat.BLL.Repository;
using Sneat.DAL.Context;
using Sneat.DAL.Entity;
using Sneat.PL.Fillter;
using Sneat.PL.Helper;
using Sneat.PL.Mapper;
using Sneat.PL.Setting;

namespace Sneat.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<SneatdbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("SneatConnection"));
			});

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IMailSetting, EmailSettings>();
			builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
			builder.Services.AddAutoMapper(M => M.AddProfile(new DomainProfile()));
            builder.Services.AddTransient<IAuthorizationHandler, PermissionAuthHandler>();
           // builder.Services.AddScoped<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				option.Password.RequireNonAlphanumeric = true;
				option.Password.RequireDigit = true;
				option.Password.RequireUppercase = true;
				option.Password.RequireLowercase = true;
			})
				.AddEntityFrameworkStores<SneatdbContext>()
				.AddDefaultTokenProviders();
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => 
			{
				option.LogoutPath = "Account/Login";
				option.AccessDeniedPath = "Home/Error";
			});

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

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");

			app.Run();
		}
	}
}