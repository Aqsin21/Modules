﻿using Microsoft.EntityFrameworkCore;
using Modules.Data;
using System.Configuration;


namespace Modules
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Veritabanı bağlantı dizesi
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Diğer servislerinizi buraya ekleyin
            services.AddControllersWithViews();
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AccountController}/{action=Register}/{id?}");

            app.Run();
        }
    }
}