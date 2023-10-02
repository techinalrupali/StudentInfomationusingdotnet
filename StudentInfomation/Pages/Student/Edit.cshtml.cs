using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentInfomation.Pages.Student
{
    public class EditModel : PageModel
    {
        public StudInfo studInfo = new StudInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=DESKTOP-KKEEJ3M\\SQLEXPRESS01;Initial Catalog=Test;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from stud where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studInfo.id = "" + reader.GetInt32(0);
                                studInfo.Sname = reader.GetString(1);
                                studInfo.fathername = reader.GetString(2);
                                studInfo.mothername = reader.GetString(3);
                                studInfo.email = reader.GetString(4);
                                studInfo.age = "" + reader.GetInt32(5);
                                studInfo.phone = reader.GetString(6);
                                studInfo.saddress = reader.GetString(7);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }
        }
        public void OnPost() 

        {
            studInfo.id = Request.Form["id"];
            studInfo.Sname = Request.Form["name"];
            studInfo.fathername = Request.Form["fname"];
            studInfo.mothername = Request.Form["mname"];
            studInfo.email = Request.Form["email"];
            studInfo.age = Request.Form["age"];
            studInfo.phone = Request.Form["phone"];
            studInfo.saddress = Request.Form["address"];

            if (studInfo.id.Length==0|| studInfo.Sname.Length == 0 || studInfo.fathername.Length == 0 || studInfo.mothername.Length == 0 || studInfo.email.Length == 0 || studInfo.age.Length == 0 ||
              studInfo.phone.Length == 0 || studInfo.saddress.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-KKEEJ3M\\SQLEXPRESS01;Initial Catalog=Test;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "update stud " +
                        "set Sname=@name, fathername=@fname, mothername=@mname, email=@email, age=@age, phone=@phone, saddress=@saddress " +
                        "where id=@id";

                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.AddWithValue("@id", studInfo.id);
                        com.Parameters.AddWithValue("@name", studInfo.Sname);
                        com.Parameters.AddWithValue("@fname", studInfo.fathername);
                        com.Parameters.AddWithValue("@mname", studInfo.mothername);
                        com.Parameters.AddWithValue("@email", studInfo.email);
                        com.Parameters.AddWithValue("@age", studInfo.age);
                        com.Parameters.AddWithValue("@phone", studInfo.phone);
                        com.Parameters.AddWithValue("@saddress", studInfo.saddress);
                        

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Student/Index");

        }
    }
}
