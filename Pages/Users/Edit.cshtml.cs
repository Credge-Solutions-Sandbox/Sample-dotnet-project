using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace m.Pages.Users
{
    public class Edit : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty,Required(ErrorMessage ="The First name is required")]
        public string FirstName { get; set; }="";
        
        [BindProperty,Required(ErrorMessage ="The Last name is required")]
        public string LastName { get; set; }="";
        
        [BindProperty,Required,EmailAddress]
        public string Email { get; set; }="";
        
        [BindProperty,Phone]
        public string? Phone { get; set; }

        public string ErrorMessage { get; set; }="";
        
        public void OnGet( int id)
        {
            try
            {

                string connectionString= "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql="Select * From customer WHERE id = @id";
                    using(SqlCommand commamnd = new SqlCommand(sql, connection))
                    {
                        commamnd.Parameters.AddWithValue("@id",id);
                        using(SqlDataReader reader=commamnd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                FirstName = reader.GetString(1);
                                LastName = reader.GetString(2);
                                Email = reader.GetString(3);
                                Phone = reader.GetString(4);
                            }
                            else
                            {
                                Response.Redirect("/User/Index");
                            }
                        }
                    }
                }
            }
            catch(Exception ex){
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost( )
        {
            if(!ModelState.IsValid)
            {
                return;
            }

            if(Phone==null)Phone="";
            //update

            try{
                string connectionString= "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection=new SqlConnection(connectionString)){
                    connection.Open();

                    string sql="Update customer Set firstname=@firstname, lastname=@lastname,email=@email Phone=@phone WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@firstname",FirstName);
                        command.Parameters.AddWithValue("@lastname",LastName);
                        command.Parameters.AddWithValue("@email",Email);
                        command.Parameters.AddWithValue("@phone",Phone);
                        command.Parameters.AddWithValue("@id",Id);

                        command.ExecuteNonQuery();
                        
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Users/Index");
        }
    }
}