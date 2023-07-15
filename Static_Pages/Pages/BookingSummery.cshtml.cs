using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Static_Pages.Pages.Data;
namespace Static_Pages.Pages
{
    public class BookingSummeryModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string ContactNumber { get; set; }

        [BindProperty]
        public string TrekName { get; set; }

        [BindProperty]
        public int NumberOfTrekkers { get; set; }

        [BindProperty]
        public decimal TotalPrice { get; set; }

        public bool IsPosted { get; set; }

        

        [BindProperty]
        public int Price { get; set; }

       


        public BookingSummeryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            IsPosted = false;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookingDataAccess = new BookingDataAccess(_configuration);
                bookingDataAccess.SaveBooking(FullName, Email, ContactNumber, TrekName, NumberOfTrekkers, TotalPrice);

               
                TotalPrice = 1200 * NumberOfTrekkers;
                IsPosted = true;
                return Page();

               
            }
            else
            {
                return Page();
            }
        }
    }
}

