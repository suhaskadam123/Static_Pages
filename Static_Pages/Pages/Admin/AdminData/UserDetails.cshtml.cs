using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Static_Pages.Pages.Admin.AdminData
{
    public class UserDetailsModel : PageModel
    {
        public DataSet UserInfo { get; set; }

        public void OnGet()
        {
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Users";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        UserInfo = new DataSet();
                        sda.Fill(UserInfo);
                    }

                }
            }
        }
    }
}
        
    
