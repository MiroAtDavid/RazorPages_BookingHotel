using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookMe.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace BookMe.WebApp.Pages.User;

public class Index : PageModel {
    
    private readonly UserRepository _users;

    public Index(UserRepository users) {
        _users = users;
    }

    public IEnumerable<Model.User> Users =>
        _users.Set
            .Include(u => u.Hotels)
            .OrderBy(u => u.Usertype).ThenBy(u => u.Username);
    public void OnGet()
    {

    }
}
