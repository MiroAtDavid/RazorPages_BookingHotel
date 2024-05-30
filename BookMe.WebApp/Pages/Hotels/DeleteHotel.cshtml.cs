using AutoMapper;
using BookMe.Infrastructure.Repositories;
using BookMe.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BookMe.WebApp.Pages.Hotels;    

public class DeleteHotel : PageModel {
    
    // TODO implement Delete propperly eg Debug, thanks future me, sorry for not doing it now
    private readonly HotelRepository _hotels;
    [TempData]
    public string? Message { get; set; }
    public Hotel Hotel { get; set; } = default!;
    
    public DeleteHotel(HotelRepository hotels){
        _hotels = hotels;
    }

    public IActionResult OnPostCancel() => RedirectToPage("/Hotels/Index");

    public IActionResult OnPostDelete(Guid guid) {
        var hotel = _hotels.FindById(guid);
        if (hotel is null) 
            return RedirectToPage("/Hotels/Index");
        var (successs, message) = _hotels.Delete(hotel);
        if (!successs) 
            Message = message;
        return RedirectToPage("/Hotels/Index");
    }
    
    public IActionResult OnGet(Guid guid) {
        var hotel = _hotels.FindById(guid);
        if (hotel is null) 
            return RedirectToPage("/Hotels/Index");
        Hotel = hotel;
        return Page();
    }
}