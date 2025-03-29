using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;
using HotelRepositories.Repository;
using Microsoft.EntityFrameworkCore;
using HotelWebApp.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddDbContext<HotelDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
builder.Services.AddScoped<AccountDAO>();
builder.Services.AddScoped<RoomDAO>();


builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});
app.UseSession();
app.UseMiddleware<AuthorizationMiddleware>();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
