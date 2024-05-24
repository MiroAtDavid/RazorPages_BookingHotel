using BookMe.Infrastructure;
using Microsoft.EntityFrameworkCore;

// Erstellen und seeden der Datenbank
var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=bookings.db")  // Keep connection open (only needed with SQLite in memory db)
    .Options;
using (var db = new BookingContext(opt)) {
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<BookingContext>(opt => {
    opt.UseSqlite("Data Source=bookings.db");
});

builder.Services.AddRazorPages();

// Configure the HTTP request pipeline.
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