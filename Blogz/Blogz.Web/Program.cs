using Blogz.Web.Data;
using Blogz.Web.Models.Domain.Entities;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogsDbConnectionString")));

builder.Services.AddDbContext<BlogsAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogsAuthDbConnectionString")));

builder.Services.AddIdentity<ApplicationUser, Role>()
    .AddEntityFrameworkStores<BlogsAuthDbContext>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, ImageCloudinaryRepository>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();