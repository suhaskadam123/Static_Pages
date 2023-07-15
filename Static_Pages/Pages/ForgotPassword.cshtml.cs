using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
namespace Static_Pages.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly string _connectionString;

        public ForgotPasswordModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [BindProperty]
        public ForgotPasswordViewModel User { get; set; }

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
            // Check if the provided email exists in the database
            bool emailExists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", User.Email);

                    int count = (int)command.ExecuteScalar();
                    emailExists = count > 0;
                }
            }

            if (emailExists)
            {
                // Send password reset email to the user
                // Implement your logic here for sending the email
                // You can use a library like SendGrid or SmtpClient to send the email

                return RedirectToPage("/Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email address");
                return Page();
            }
        }
    }

    public class ForgotPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}