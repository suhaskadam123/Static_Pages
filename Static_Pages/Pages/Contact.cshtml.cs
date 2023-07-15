using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Static_Pages.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public ContactModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Contact (Name, Email, Message) VALUES (@Name, @Email, @Message)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Message", Message);

                        command.ExecuteNonQuery();
                    }
                }

                ViewData["Message"] = "Thank you for contacting us!";
            }

            return Page();
        }
    }
}

