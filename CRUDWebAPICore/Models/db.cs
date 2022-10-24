using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace CRUDWebAPICore.Models
{
    public class db
    {
        //SqlConnection con = new SqlConnection(@"Data Source=AK-SWL-0147\DEV;Initial Catalog=SAM;Integrated Security=True");
        SqlConnection con = new SqlConnection(@"Data Source =AK-SWL-0147\DEV;Initial Catalog = SAM; Integrated Security = True");
        public Task<string> EmployeeOpt(Employee emp)
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("Sp_employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID",emp.ID);
                com.Parameters.AddWithValue("@Email", emp.Email);
                com.Parameters.AddWithValue("@Emp_Name", emp.Emp_Name);
                com.Parameters.AddWithValue("@DesignationID", emp.DesignationID);
                com.Parameters.AddWithValue("@type", emp.type);

                con.Open();
               int g = com.ExecuteNonQuery();
                con.Close();

                if (g>0)
                {
                    msg = "Success";
                }
                else
                {
                    msg = "Unsuccess";
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                if (con.State==ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return Task.FromResult(msg);

        }

        public DataSet EmployeeGet(Employee emp, out string msg)
        {
             msg = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("Sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", emp.ID);
                com.Parameters.AddWithValue("@Email", emp.Email);
                com.Parameters.AddWithValue("@Emp_Name", emp.Emp_Name);
                com.Parameters.AddWithValue("@DesignationID", emp.DesignationID);
                com.Parameters.AddWithValue("@type", emp.type);
                //com.Parameters.AddWithValue("@DesignationName", emp.DesignationName);
                //con.Open();
                //com.ExecuteNonQuery();
                //con.Close();

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                msg = "Success";

            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return ds;
        }
    }
}
