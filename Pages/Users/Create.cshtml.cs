using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace m.Pages.Users
{
    public class Create : PageModel
    {
        [BindProperty,Required(ErrorMessage ="The First name is required")]
        public string FirstName { get; set; }="";
        
        [BindProperty,Required(ErrorMessage ="The Last name is required")]
        public string LastName { get; set; }="";
        
        [BindProperty,Required,EmailAddress]
        public string Email { get; set; }="";
        
        [BindProperty,Phone]
        public string? Phone { get; set; }
        


        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; }="";
        public void OnPost()
        {
            if(!ModelState.IsValid){
                return;
            }

            if(Phone==null)Phone="";

            //create new user

            try{
                string connectionString= "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection=new SqlConnection(connectionString)){
                    connection.Open();

                    string sql="INSERT INTO customer"+
                    "(firstname, lastname,email,phone)Values"+
                    "(@firstname,@lastname,@email,@phone)";

                    using(SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@firstname",FirstName);
                        command.Parameters.AddWithValue("@lastname",LastName);
                        command.Parameters.AddWithValue("@email",Email);
                        command.Parameters.AddWithValue("@phone",Phone);

                        command.ExecuteNonQuery();
                    }

                }
            }catch(Exception ex)
            {   
                ErrorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Users/Index");



        }
    }
}