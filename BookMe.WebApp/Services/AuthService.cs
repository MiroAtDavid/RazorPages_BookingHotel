using BookMe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BookMe;

public class AuthService
    {
        private readonly bool _isDevelopment;
        private readonly BookingContext _db;
        private readonly ICryptService _crypt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(bool isDevelopment,
            BookingContext db, ICryptService crypt, IHttpContextAccessor httpContextAccessor)
        {
            _isDevelopment = isDevelopment;
            _db = db;
            _crypt = crypt;
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpContext => _httpContextAccessor?.HttpContext
            ?? throw new NotSupportedException();

        public async Task<(bool success, string message)> TryLoginAsync(string username, string password)
        {
            var dbUser = _db.Users.FirstOrDefault(u => u.Username == username);
            if (dbUser is null) { return (false, "Unknown username or wrong password."); }
            var passwordHash = _crypt.GenerateHash(dbUser.Salt, password);
            if (!_isDevelopment && passwordHash != dbUser.PasswordHash)
            {
                return (false, "Unknown username or wrong password.");
            }
            var role = dbUser.Usertype.ToString();
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role),
                    //new Claim("Userdata", JsonSerializer.Serialize(currentUser)),
                };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(4),
            };

            await HttpContext.SignInAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return (true, string.Empty);
        }
        public bool IsAuthenticated => HttpContext.User.Identity?.Name != null;
        public string? Username => HttpContext.User.Identity?.Name;
        public bool HasRole(string role) => HttpContext.User.IsInRole(role);
        public bool IsAdmin => HttpContext.User.IsInRole(Model.Usertype.Admin.ToString());
        public Task LogoutAsync() => HttpContext.SignOutAsync();
        //public Userdata? Userdata
        //{
        //    get
        //    {
        //        var userdata = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Userdata")?.Value;
        //        return string.IsNullOrEmpty(userdata) ? null : JsonSerializer.Deserialize<Userdata>(userdata);
        //    }
        //}
    }