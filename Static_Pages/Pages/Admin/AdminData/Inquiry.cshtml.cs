using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Static_Pages.Pages.Admin.AdminData
{
    public class InquiryModel : PageModel
    {
        public DataSet Inquiry { get; set; }

        public void OnGet()
        {
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TrekBooking;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Contact";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        Inquiry = new DataSet();
                        sda.Fill(Inquiry);
                    }
                }
            }
        }
    }
}
