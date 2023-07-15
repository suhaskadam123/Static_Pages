using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Static_Pages.Pages.Admin_pages
{
    public class feedbackModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
      
        public string? Password { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }
       

        public void OnGet()
        {
        }

       
        public IActionResult OnPost()
        {
            if (Username == "suhas" && Password == "kadam")
            {
                return RedirectToPage("Adminfirstpage");


            }
            else
            {
                ErrorMessage = "Please provide both username and password.";
                return Page();
            }


        }
       
    }

}




