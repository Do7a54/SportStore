using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.AspNetCore.Identity;  // Page 269

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});             // Added Page 150 

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();  // added to to create a service for the IStoreRepository interface that uses EFStoreRepository at page 152

builder.Services.AddScoped<IOrderRepository, EFOrderRepository>(); // Page 234 -- Service For Orders

builder.Services.AddRazorPages();     //Enabling Razor Pages in the Program.cs File Page 198

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();   // Enable Sessions Page 201 

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();  // Services For Cart Class Page 218

builder.Services.AddServerSideBlazor(); // Added Page 244 -- Enabling Blazor

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
options.UseSqlServer(
builder.Configuration["ConnectionStrings:IdentityConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<AppIdentityDbContext>();  // Page 269 -- Identity

var app = builder.Build();
//app.MapGet("/", () => "Hello World!");

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/error");
}
app.UseRequestLocalization(opts => {
    opts.AddSupportedCultures("en-US")
    .AddSupportedUICultures("en-US")
    .SetDefaultCulture("en-US");
});                        /// Configuring Error Handling in the Program.cs File -- Page 281

app.UseStaticFiles();

app.UseSession();         // Enable Sessions Page 201

app.UseAuthentication();
app.UseAuthorization();   // 2 Lines Added at Page 269 -- Identity

app.MapControllerRoute("catpage",
"{category}/Page{productPage:int}",
new { Controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Page{productPage:int}",
new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("category", "{category}",
new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("pagination",
"Products/Page{productPage}",
new { Controller = "Home", action = "Index", productPage = 1 }); // 4 orders added at page 183 to change route debendent on categories

//app.MapControllerRoute("pagination",
//"Products/Page{productPage}",
//new { Controller = "Home", action = "Index" }); //Page 171 : To Change the Route

app.MapDefaultControllerRoute();

//app.UseAuthorization();     // Added By Me -- To Solve Problem in page 280

app.MapRazorPages();      //Enabling Razor Pages in the Program.cs File Page 198

app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index"); // Page 244 -- Enabling Blazor

SeedData.EnsurePopulated(app);    // Added after adding SeedData model page 155

IdentitySeedData.EnsurePopulated(app);  // Seeding the Identity Database in the Program.cs -- Page 272

app.Run();

//Comment For GitHub Commit