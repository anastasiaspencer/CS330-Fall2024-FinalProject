using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using CS330_Fall2024_FinalProject.Core.Repositories;
using CS330_Fall2024_FinalProject.Repositories;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Athlete>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true; //change this later
        options.Tokens.EmailConfirmationTokenProvider = "Default";
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies();

#endregion

AddScoped();

builder.Services.AddHttpClient<ISnowReportService, SnowReportService>();
builder.Services.AddScoped<SnowReportApplicationService>();


builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<AthleteApplicationService>();

builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddScoped<SkiStatsRepository>();



// for email service
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Athlete", "Coach", "Admin" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

app.Run();

//Add authorization and add athlete only policy that requires the athletenumber claim as a requirement
void AddAuthorizationPolicies(){
    // builder.Services.AddAuthorization(options =>
    // {
    //     options.AddPolicy("AthleteOnly", policy => policy.RequireClaim("AthleteNumber"));
    // });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireCoach", policy => policy.RequireRole("Coach"));
        options.AddPolicy("RequireManager", policy => policy.RequireRole("Manager"));
        options.AddPolicy("RequireAthlete", policy => policy.RequireRole("Athlete"));
    });
}

void AddScoped(){
    builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}