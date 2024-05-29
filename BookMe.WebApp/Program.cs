using BookMe;
using BookMe.Dto;
using BookMe.Infrastructure;
using BookMe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookMe.Dto;
using BookMe.Infrastructure;
using BookMe.Infrastructure.Repositories;
using BookMe.Model;
//using BookMe.Services;
//using BookMe.Services;
// Erstellen und seeden der Datenbank
var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=bookings.db")  // Keep connection open (only needed with SQLite in memory db)
    .Options;
using (var db = new BookingContext(opt)) {
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed(new CryptService());
}
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<BookingContext>(opt => {
    opt.UseSqlite("Data Source=bookings.db");
});

// * Repositories **********************************************************************************
builder.Services.AddTransient<HotelRepository>();
builder.Services.AddTransient<BookingRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<EmployeeRepository>();
builder.Services.AddTransient<RoomRepository>();
builder.Services.AddTransient<GuestRepository>();
builder.Services.AddTransient<AddressRepository>();


// * Services for authentication *******************************************************************
// To access httpcontext in services
builder.Services.AddHttpContextAccessor();
// Hashing methods
builder.Services.AddTransient<ICryptService, CryptService>();
builder.Services.AddTransient<AuthService>(provider => new AuthService(
    isDevelopment: builder.Environment.IsDevelopment(),
    db: provider.GetRequiredService<BookingContext>(),
    crypt: provider.GetRequiredService<ICryptService>(),
    httpContextAccessor: provider.GetRequiredService<IHttpContextAccessor>()));
builder.Services.AddAuthentication(
        Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/User/Login";
        o.AccessDeniedPath = "/User/AccessDenied";
    });
    
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("OwnerOrAdminRole", p => p.RequireRole(Usertype.Owner.ToString(), Usertype.Admin.ToString()));
});


// * Other Services ********************************************************************************
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddRazorPages();



// Configure the HTTP request pipeline.
// *************************************************************************************************
// MIDDLEWARE
// *************************************************************************************************
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();