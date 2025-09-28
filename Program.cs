using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// 1. Configure SQL connection string
// ----------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=GiftOfTheGiversDB;Trusted_Connection=True;MultipleActiveResultSets=true";

// ----------------------
// 2. Add DbContext with SQL Server
// ----------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ----------------------
// 3. Add Identity (with EF Core storage)
// ----------------------
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

// ----------------------
// 4. Register custom services
// ----------------------
builder.Services.AddScoped<IUserOperations, TUserOperations>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddScoped<IVolunteerService, VolunteerService>();

// ----------------------
// 5. Add MVC controllers with views
// ----------------------
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ----------------------
// Middleware pipeline
// ----------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication + Authorization
app.UseAuthentication();
app.UseAuthorization();

// ----------------------
// Default route
// ----------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run application
app.Run();
