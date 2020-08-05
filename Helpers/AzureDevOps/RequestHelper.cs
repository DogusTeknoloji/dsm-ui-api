using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers.AzureDevOps
{
    public static class RequestHelper
    {
        public static string AzureDevOpsToken;
        public static string AzureDevOpsOrganizationName;
        public static async Task<string> Evaluate(string url)
        {
            string pAccessToken = AzureDevOpsToken;
            string pAccessTokenB64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + pAccessToken));

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", pAccessTokenB64);

                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
        }
    }
}
