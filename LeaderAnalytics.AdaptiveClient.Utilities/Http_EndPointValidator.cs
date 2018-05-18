using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;


namespace LeaderAnalytics.AdaptiveClient.Utilities
{
    public class Http_EndPointValidator : IEndPointValidator
    {
        public virtual bool IsInterfaceAlive(IEndPointConfiguration endPoint)
        {
            return IsInterfaceAlive(endPoint.ConnectionString);
        }

        public virtual bool IsInterfaceAlive(string url)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            bool success = false;
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            HttpResponseMessage response = httpClient.SendAsync(msg).Result;
            success = response.StatusCode == HttpStatusCode.OK;
            return success;
        }
    }
}
