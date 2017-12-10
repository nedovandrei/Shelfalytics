using Newtonsoft.Json;
using Shelfalytics.RepositoryInterface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.Repository.Repositories
{
    public class SShelfRepository : ISShelfRepository
    {
        private readonly string EndPoint = "http://smart-chillers.tk/api/auth/?";
        private readonly string Param1 = "user=";
        private readonly string Param2 = "pass=";
        private readonly string UserName = "apiuser";
        private readonly string Password = "api321pass";
        private string Token;
        private readonly HttpClient client = new HttpClient();

        public SShelfRepository()
        {
             Authorize();
        }

        public async Task Authorize()
        {
            var responseString = await client.GetStringAsync(EndPoint + Param1 + UserName + "&" + Param2 + Password);
            //JsonConvert.DeserializeAnonymousType(responseString, data);
            var data = JsonConvert.DeserializeObject<SShelfAuthResponse>(responseString);
            if (!data.Error)
            {
                Token = data.Token;
            }
            else
            {
                throw new Exception("error authenticating");
            }
        }
    }

    internal class SShelfAuthResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
