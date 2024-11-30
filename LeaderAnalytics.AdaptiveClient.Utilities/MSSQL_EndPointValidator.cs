﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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

        public virtual async Task<bool> IsInterfaceAliveAsync(IEndPointConfiguration endPoint)
        {
            bool result = true;

            using (SqlConnection con = new SqlConnection(endPoint.ConnectionString))
            {
                try
                {
                    await con.OpenAsync();
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
