using System.Data;

namespace CRUDWebAPICore.Models
{
    public class Common
    {
    //     if (dr.Table.Columns.Contains("Email"))
    //            {
    //                return dr["Email"].ToString();
    //}
    //            else {

    //                return null;
    //            }


public T checkModelKey<T>(T value,DataRow dr)
        {
            
            if (dr.Table.Columns.Contains("Email"))
            {
                //return dr["Email"].ToString();
            }
            else
            {

                //return null;    
            }
            return value;

        }
    }
}


