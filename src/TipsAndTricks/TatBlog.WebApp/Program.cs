using Microsoft.AspNetCore.Mvc;
using TatBlog.WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.ConfigureMvc()
           .ConfigureServices();
}
var app = builder.Build();
{
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseDeveloperExceptionPage();
    //}
    //else
    //{
    //    app.UseExceptionHandler("/Blog/Error");
    //    app.UseHsts();
    //}
    app.UseRequestPipeline(); 
    app.UseBlogRoutes(); 
    app.UseDataSeeder();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();

app.MapControllerRoute(
    name: "Posts-by-category", 
    pattern:"blog/category{slug}",
    defaults:new {Controller ="Blog", action = "Category"});

app.MapControllerRoute(
    name: "Posts-by-tag",
    pattern: "blog/tag{slug}",
    defaults: new { Controller = "Blog", action = "Tag" });

app.MapControllerRoute(
    name: "single-post",
    pattern: "blog/tag{year:int}/{month:int}/{day:int}/{slug}",
    defaults: new { Controller = "Blog", action = "Post" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");


