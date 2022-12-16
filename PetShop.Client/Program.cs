using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Data.Repository;
using PetShop.Service;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationContext = PetShop.Data.Contexts.AuthenticationContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PetShopDataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("PetShopDataConnection")));

builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlite("Data Source=c:\\temp\\user.db"));
builder.Services.AddIdentity<IdentityUser,IdentityRole>()
        .AddEntityFrameworkStores<AuthenticationContext>();

builder.Services.AddScoped<IAnimalReposetory, AnimalRepository>();
builder.Services.AddScoped<ICommentReposiTory, CommentRepository>();
builder.Services.AddTransient<UserService>();


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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Starting}/{id?}");

app.Run();
