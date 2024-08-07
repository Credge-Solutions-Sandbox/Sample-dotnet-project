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
    public class Index : PageModel
    {
        public List<UserInfo> UsersList{get; set;}=[];

        public void OnGet()
        {
            try{
                string connectionString= "Server=.;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection=new SqlConnection(connectionString)){
                    connection.Open();

                    string sql= "Select * From customer ORDER BY ID ";

                    using(SqlCommand command = new SqlCommand(sql, connection)){
                        using(SqlDataReader reader=command.ExecuteReader()){
                            while(reader.Read()){
                                UserInfo userInfo = new UserInfo();
                                userInfo.Id = reader.GetInt32(0);
                                userInfo.FirstName = reader.GetString(1);
                                userInfo.LastName = reader.GetString(2);
                                userInfo.Email = reader.GetString(3);
                                userInfo.Phone = reader.GetString(4);
                                userInfo.CreatedAt=reader.GetDateTime(5).ToString("MM/dd/yyyy");

                                UsersList.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex){
                Console.WriteLine("We have an Error "+ex.Message);
            }
        }
    }

    public class UserInfo{
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string CreatedAt { get; set; } = "";
    }
}