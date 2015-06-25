using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using HRIMS;
namespace HRMS
{
    
  public class Menu
    {
        
        public static DataTable MenuTable()
        {
            try
            {
                DataTable dt=null;
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    int id=Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    cEmpLogin emp = cEmpLogin.Get_ID(id);
                    int accessId = emp.objRoleAccess.iObjectID;
                    string strCon = ConfigurationManager.ConnectionStrings["DevA4.DatabaseConnectionString"].ConnectionString;
                    SqlConnection conn = new SqlConnection(strCon);
                    SqlDataAdapter adpt = new SqlDataAdapter("uspMenu", conn);
                    adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adpt.SelectCommand.Parameters.AddWithValue("@id",accessId);
                    conn.Open();
                    adpt.SelectCommand.ExecuteReader();
                    conn.Close();
                    dt = new DataTable();
                    adpt.Fill(dt);
                    
                }
                return dt;
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
    }
}
