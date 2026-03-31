using HotelManagment.Data;
using HotelManagment.Models;
<<<<<<< Updated upstream
=======
using HotelManagment.Services;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< Updated upstream

builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>() 
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

=======

builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>() 
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

>>>>>>> Stashed changes
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedAdmin(services);
}

app.Run();

app.Run();
