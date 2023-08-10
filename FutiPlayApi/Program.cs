using FutiPlay.Api.Data;
using FutiPlay.Core.Bac;
using FutiPlay.Core.Identity.Bac;
using FutiPlay.Core.Identity.Enums;
using FutiPlay.Core.Identity.Models;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
string? defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

// JWT Token Security Configuration
IConfigurationSection jwtSection = configuration.GetSection("JWTSettings");

builder.Services.Configure<JWTKey>(jwtSection);
JWTKey? appSettings = jwtSection.Get<JWTKey>();

byte[] criptographedKeyInBytes = Encoding.ASCII.GetBytes(appSettings!.SecretKey);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(auth =>
{
    auth.RequireHttpsMetadata = false;
    auth.SaveToken = true;
    auth.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(criptographedKeyInBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


// Add services to the container.
builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:4200", "http://192.168.0.204:3000").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Inject IDbConnection, with implemantation from SqlConnection class
builder.Services.AddTransient<IDbConnection>(config => new SqlConnection(defaultConnectionString));

// Entity and Identity Framework configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.AllowedForNewUsers = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection for Sign in
builder.Services.AddScoped<UserManager<ApplicationUser>>();

// Authorization and Registration Injections
builder.Services.AddScoped<IIdentityBac, IdentityBac>();

// life cycle dependency injection
builder.Services.AddSingleton<IPlayerBac, PlayerBac>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();

builder.Services.AddSingleton<ITournamentBac, TournamentBac>();
builder.Services.AddSingleton<ITournamentRepository, TournamentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

// Keeps this order: Authentication and then Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Scope for creating default roles
using (IServiceScope scope = app.Services.CreateScope())
{
    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string?[] roles = new[] 
    { 
        RoleEnum.SysAdmin.Name,
        RoleEnum.NormalUser.Name,
        RoleEnum.TournamentOwner.Name,
        RoleEnum.TeamOwner.Name,
        RoleEnum.Coach.Name,
        RoleEnum.Player.Name,
        RoleEnum.Referee.Name
    };

    foreach (string? role in roles)
    {
        if (role != null && !await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// Scope for creating default roles
using (IServiceScope scope = app.Services.CreateScope())
{
    UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string email = "superadmin@admin.com";
    string password = "Test#1234";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        ApplicationUser appUser = new()
        {
            UserName = email,
            Email = email
        };

        IdentityResult result = await userManager.CreateAsync(appUser, password);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(appUser, nameof(RoleEnum.SysAdmin));
    }
}

app.Run();
