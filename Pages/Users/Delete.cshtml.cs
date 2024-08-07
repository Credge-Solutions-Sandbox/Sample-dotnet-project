using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace m.Pages.Users
{
    public class Delete : PageModel
    {
        public void OnGet()
        {   
        }

        public void OnPost(int id)
        {
            deleteUser(id);
            Response.Redirect("/Users/Index");
        }
        private void deleteUser(int id)
        {
            try{
                string connectionString= "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection=new SqlConnection(connectionString)){
                    connection.Open();

                    //delete user

                    string sql = "DELETE FROM customer WHERE Id = @id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id",id);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Cannot delete User:"+ ex.Message);
            }
        }
    }
}