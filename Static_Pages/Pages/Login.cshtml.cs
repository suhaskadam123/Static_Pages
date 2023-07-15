using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Static_Pages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly string _connectionString;

        public LoginModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [BindProperty]
        public UserLogin User { get; set; }

        public void OnGet()
        {
            // Perform any necessary initialization for the page
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True";
            bool isAuthenticated = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", User.Username);
                    command.Parameters.AddWithValue("@Password", User.Password);

                    int count = (int)command.ExecuteScalar();
                    isAuthenticated = count > 0;
                }
            }

            if (isAuthenticated)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }
    }

    public class UserLogin
    {
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}