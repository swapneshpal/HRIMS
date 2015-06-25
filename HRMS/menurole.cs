using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using HRIMS;
namespace HRMS
{

   public class menurole
    {
       public static DataTable getMenu()
       {
           try
           {
               int id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
               cEmpLogin emp = cEmpLogin.Get_ID(id);
               int accessId = emp.objRoleAccess.iObjectID;
               string cs = System.Configuration.ConfigurationManager.ConnectionStrings["DevA4.DatabaseConnectionString"].ConnectionString;

               SqlConnection con = new SqlConnection(cs);
               SqlDataAdapter ad = new SqlDataAdapter("uspfindMenu", con);
               ad.SelectCommand.CommandType = CommandType.StoredProcedure;
               ad.SelectCommand.Parameters.AddWithValue("@id", accessId);
               con.Open();
               ad.SelectCommand.ExecuteReader();
               con.Close();
               DataTable dt = null;
               dt = new DataTable();
               ad.Fill(dt);
               return dt;
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
         
       }
      

    }
}
