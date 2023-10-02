using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentInfomation.Pages.Student
{
    public class IndexModel : PageModel
    {
        public List<StudInfo> StI=new List<StudInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-KKEEJ3M\\SQLEXPRESS01;Initial Catalog=Test;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    string sql = "Select * from stud";
                    using(SqlCommand command = new SqlCommand(sql,connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                StudInfo studInfo = new StudInfo();
                                studInfo.id = "" + reader.GetInt32(0);
                                studInfo.Sname = reader.GetString(1);   
                                studInfo.fathername= reader.GetString(2);   
                                studInfo.mothername= reader.GetString(3);   
                                studInfo.email= reader.GetString(4);
                                studInfo.age = "" + reader.GetInt32(5);
                                studInfo.phone= reader.GetString(6);
                                studInfo.saddress= reader.GetString(7);
                                studInfo.Regisdate = reader.GetDateTime(8).ToString();
                                
                                StI.Add(studInfo);  
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: "+ex.ToString());
            }
        }
    }
    public class StudInfo
    {
        public string id;
        public string Sname;
        public string fathername;
        public string mothername;
        public string email;
        public string phone;
        public string age;
        public string saddress;
        public string Regisdate;
    }
}
