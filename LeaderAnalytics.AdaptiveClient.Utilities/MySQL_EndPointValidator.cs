﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;


namespace LeaderAnalytics.AdaptiveClient.Utilities
{
    public class MySQL_EndPointValidator : IEndPointValidator
    {
        public virtual bool IsInterfaceAlive(IEndPointConfiguration endPoint)
        {
            bool result = true;

            using (MySqlConnection con = new MySqlConnection(endPoint.ConnectionString))
            {
                try
                {
                    con.Open(); // must specify an existing database name in connection string or it throws.
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
