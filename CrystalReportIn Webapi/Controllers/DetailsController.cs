using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrystalReportIn_Webapi.Models;
using System.IO;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Web.Http.Cors;

namespace CrystalReportIn_Webapi.Controllers
{
    [RoutePrefix("api/Details")]
    public class DetailsController : ApiController
    {
        CodeXEntities cX = new CodeXEntities();

        [AllowAnonymous]
        [Route("Report/SendReport")]
        [HttpPost]
        public HttpResponseMessage ExportReport(Users user)
        {
            string EmailTosend = WebUtility.UrlDecode(user.Email);
            List<Users> model = new List<Users>();
            var data = cX.tbl_Registration; 
            var rd = new ReportDocument();
            
            foreach (var details in data)
            {
                Users obj = new Users();
                obj.Email = details.Email;
                obj.FirstName = details.FirstName;
                obj.LastName = details.LastName;
                model.Add(obj);

            }
            
            rd.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports"), "UserRegistration.rpt"));

            rd.SetDataSource(model);

            using (var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                SmtpClient smtp = new SmtpClient
                {
                    Port = 587,
                    UseDefaultCredentials = true,
                    Host = "smtp.gmail.com",
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
                smtp.UseDefaultCredentials = false;
                // to use google smtp to send out the report, update the credential below
                smtp.Credentials = new NetworkCredential("accountname@gmail.com", "password");
                var message = new System.Net.Mail.MailMessage("accountname@gmail.com", EmailTosend, "User Registration Details", "Hi Please check your Mail  and find the attachement.");
                message.Attachments.Add(new Attachment(stream, "UsersRegistration.pdf"));

                smtp.Send(message);
            }

            var Message = string.Format("Report Created and sended to your Mail.");
            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK, Message);
            return response1;
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        [AllowAnonymous]
        [Route("Report/DownloadReport")]
        [HttpGet]
        public HttpResponseMessage DownloadReport(Users user)
        {
            // string EmailTosend = WebUtility.UrlDecode(user.Email);
            List<Users> model = new List<Users>();
            var data = cX.tbl_Registration;
            var rd = new ReportDocument();

            foreach (var details in data)
            {
                Users obj = new Users();
                obj.Email = details.Email;
                obj.FirstName = details.FirstName;
                obj.LastName = details.LastName;
                model.Add(obj);

            }

            rd.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports"), "UserRegistration.rpt"));

            rd.SetDataSource(model);
            MemoryStream memoryStream = new MemoryStream();
            // memoryStream = (MemoryStream)rd.ExportToStream(ExportFormatType.PortableDocFormat);
            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.CopyTo(memoryStream);

            //HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK, memoryStream);
            // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Convert.ToBase64String(memoryStream.ToArray()));
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Headers.Clear();
            response.Content = new ByteArrayContent(memoryStream.ToArray());
            // response.Content = new StreamContent(stream);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition.FileName = "Transcript.pdf";

            memoryStream.Close();
            return response;
        }

    }
}
