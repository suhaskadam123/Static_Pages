using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Static_Pages.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly string _connectionString;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public AdminLoginModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True");
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                // Check if the admin credentials are valid
                if (IsAdminValid(Username, Password))
                {
                    // Store the admin data in session or perform desired operations
                    // For example, you can use TempData to pass data to another page
                    TempData["AdminUsername"] = Username;

                    // Redirect to the admin dashboard or desired page
                    return RedirectToPage("/Admin/Adminfirstpage");
                }
                else
                {
                    ErrorMessage = "Invalid username or password.";
                }
            }

            return Page();
        }

        private bool IsAdminValid(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True"))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM AdminLogin WHERE Username = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}
