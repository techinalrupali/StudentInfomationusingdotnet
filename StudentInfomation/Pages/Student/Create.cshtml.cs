using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace StudentInfomation.Pages.Student
{
    public class CreateModel : PageModel
    {
        public StudInfo studInfo=new StudInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            studInfo.Sname = Request.Form["name"];
            studInfo.fathername = Request.Form["fname"];
            studInfo.mothername = Request.Form["mname"];
            studInfo.email = Request.Form["email"];
            studInfo.age = Request.Form["age"];
            studInfo.phone = Request.Form["phone"];
            studInfo.saddress = Request.Form["address"];
            

            if(studInfo.Sname.Length==0 ||studInfo.fathername.Length==0 || studInfo.mothername.Length==0 || studInfo.email.Length==0 || studInfo.age.Length == 0||
                studInfo.phone.Length==0||studInfo.saddress.Length==0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save the new student into the database
            try
            {
                string connectionString = "Data Source=DESKTOP-KKEEJ3M\\SQLEXPRESS01;Initial Catalog=Test;Integrated Security=True";
                using (SqlConnection con=new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "insert into stud" + "(Sname,fathername,mothername,email,age,phone,saddress) values" + "(@name,@fname,@mname,@email,@age,@phone,@saddress);";

                    using (SqlCommand com=new SqlCommand(sql, con))
                    {
                        com.Parameters.AddWithValue("@name",studInfo.Sname);
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
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }
            studInfo.Sname = "";
            studInfo.fathername = "";
            studInfo.mothername = "";
            studInfo.email = "";
            studInfo.age = "";
            studInfo.phone = "";
            studInfo.saddress = "";
          

            successMessage = "New Student Added Successfully";

            Response.Redirect("/Student/Index");
        }    
    }
}
