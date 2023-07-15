
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Static_Pages.Pages.Data
{
    public class BookingDataAccess
    {
        private readonly string _connectionString;

        public BookingDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void SaveBooking(string fullName, string email, string contactNumber, string trekName, int numberOfTrekkers, decimal totalPrice)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("INSERT INTO BookingData (FullName, Email, ContactNumber, TrekName, NumberOfTrekkers, TotalPrice) VALUES (@FullName, @Email, @ContactNumber, @TrekName, @NumberOfTrekkers, @TotalPrice)", connection);

                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@TrekName", trekName);
                command.Parameters.AddWithValue("@NumberOfTrekkers", numberOfTrekkers);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);

                command.ExecuteNonQuery();
            }
        }
    }
}

