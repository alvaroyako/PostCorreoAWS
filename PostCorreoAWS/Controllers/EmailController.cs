using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCorreoAWS.Controllers
{
    public class EmailController : Controller
    {
        private IConfiguration Configuration;

        public EmailController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMailAWS()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMailAWS(string email, string subject, string body)
        {
            var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1);
            Destination destination = new Destination();
            destination.ToAddresses = new List<string> { email };
            Message message = new Message();
            message.Subject = new Content(subject);
            Body cuerpo = new Body();
            cuerpo.Html = new Content(body);
            cuerpo.Text = new Content(body);
            message.Body = cuerpo;
            SendEmailRequest request = new SendEmailRequest();
            request.Source = "alvaro.moya@tajamar365.com";
            request.Destination = destination;
            request.Message = message;
            SendEmailResponse response = await client.SendEmailAsync(request);

            ViewData["MENSAJE"] = "Mail enviado correctamente";
            return View();
        }
    }
}
