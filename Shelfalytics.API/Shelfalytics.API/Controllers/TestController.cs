using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shelfalytics.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly IMailService _mailService;
        public TestController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpGet]
        [Route("mail")]
        public async Task<HttpResponseMessage> TestMail(string to)
        {
            var product = new ProductOOSDTO
            {
                PhotoPath = "1.jpg",
                SKUName = "Very Important SKU",
                Price = 1000.00m,
                TimeStamp = DateTime.Now
            };
            await _mailService.TestSendOOSEmail(product, 5, to);

            return Request.CreateResponse(HttpStatusCode.OK, "Message sent to: " + to);
        }
    }
}
