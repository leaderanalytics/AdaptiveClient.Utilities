using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;


namespace LeaderAnalytics.AdaptiveClient.Utilities
{
    public class MSSQL_EndPointValidator : IEndPointValidator
    {
        public virtual bool IsInterfaceAlive(IEndPointConfiguration endPoint)
        {
            bool result = true;

            using (SqlConnection con = new SqlConnection(endPoint.ConnectionString))
            {
                try
                {
                    con.Open();
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
