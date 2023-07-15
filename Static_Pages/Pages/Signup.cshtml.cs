using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Static_Pages.Pages
{
    public class SignupModel : PageModel
    {
        private readonly string _connectionString;

        public SignupModel(IConfiguration configuration)
        {
            
        }

        [BindProperty]
        public UserSignup User { get; set; }

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", User.Username);
                    command.Parameters.AddWithValue("@Password", User.Password);
                    command.Parameters.AddWithValue("@Email", User.Email);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/Login");

        }
    }

    public class UserSignup
    {
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
