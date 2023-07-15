using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Static_Pages.Pages.Admin
{
    public class AdminfirstpageModel : PageModel
    {
        public string? Username { get; set; }
        public void OnGet()
        {
            
        }
    }
}
