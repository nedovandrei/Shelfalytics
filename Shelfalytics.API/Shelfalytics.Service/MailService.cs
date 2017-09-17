using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using System.Web.Hosting;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Shelfalytics.Service
{
    public class MailService : IMailService
    {
        private readonly IEquipmentDataRepository _equipmentDataRepository;

        public MailService(IEquipmentDataRepository equipmentDataRepository)
        {
            _equipmentDataRepository = equipmentDataRepository ?? throw new ArgumentNullException(nameof(equipmentDataRepository));
        }
        public async Task SendOOSEmail(ProductOOSDTO product, int equipmentId)
        {
            var users = await _equipmentDataRepository.GetEquipmentUsers(equipmentId);

            using (var client = new SmtpClient("mail.shelf.work"))
            {
                await Task.Run(delegate
                {
                    client.Port = 8889;

                    var credentials = new NetworkCredential("notify@shelf.work", "Shelf3592!");
                    client.Credentials = credentials;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = true;
                    client.Host = "mail.shelf.work";

                    var mail = new MailMessage
                    {
                        From = new MailAddress("notify@shelf.work"),
                        Subject = "Out Of Stock Notification",
                        IsBodyHtml = true,
                        //mail.Body = "<html><body><font color=\"red\">OOS NOTIFICATION TEST</font></body></html>";
                        Body = File.ReadAllText(HostingEnvironment.MapPath("~/Content/MessageTemplates/OOSNotification.html"))
                    };
                    mail.Body = mail.Body.Replace("%SKUName%", product.SKUName);
                    mail.Body = mail.Body.Replace("%Price%", product.Price.ToString());
                    mail.Body = mail.Body.Replace("%TimeStamp%", product.TimeStamp.ToString());
                    mail.Body = mail.Body.Replace("%PhotoPath%", product.PhotoPath);
                    foreach (var user in users)
                    {
                        mail.To.Add(new MailAddress(user.Email));
                    }
                    try
                    {
                        client.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        ExceptionCatcher(ex);
                    }
                });
            }

        }

        public async Task TestSendOOSEmail(ProductOOSDTO product, int equipmentId, string to)
        {
            using (var client = new SmtpClient("mail.shelf.work"))
            {
                await Task.Run(delegate
                {
                    client.Port = 8889;

                    var credentials = new NetworkCredential("notify@shelf.work", "Shelf3592!");
                    client.Credentials = credentials;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = true;
                    client.Host = "mail.shelf.work";

                    var mail = new MailMessage
                    {
                        From = new MailAddress("notify@shelf.work"),
                        Subject = "Out Of Stock Notification",
                        IsBodyHtml = true,
                        //mail.Body = "<html><body><font color=\"red\">OOS NOTIFICATION TEST</font></body></html>";
                        Body = File.ReadAllText(HostingEnvironment.MapPath("~/Content/MessageTemplates/OOSNotification.html"))
                    };
                    mail.Body = mail.Body.Replace("%SKUName%", product.SKUName);
                    mail.Body = mail.Body.Replace("%Price%", product.Price.ToString());
                    mail.Body = mail.Body.Replace("%TimeStamp%", product.TimeStamp.ToString());
                    mail.Body = mail.Body.Replace("%PhotoPath%", product.PhotoPath);
                    mail.To.Add(to);
                    try
                    {
                        client.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        ExceptionCatcher(ex);
                    }
                });
            }
        }

        private void ExceptionCatcher(Exception ex)
        {
            using (var client = new SmtpClient("mail.shelf.work"))
            {

                var credentials = new NetworkCredential("notify@shelf.work", "Shelf3592!");
                client.Credentials = credentials;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = true;
                client.Host = "mail.shelf.work";

                var mail = new MailMessage
                {
                    From = new MailAddress("notify@shelf.work"),
                    Subject = "Mailing Exception",
                    Body = "Sending Email Failed" + "\n\n" + ex.Message + "\n\n" + ex.Source + "\n\n" + ex.StackTrace
                };
                mail.To.Add("notify@shelf.work");
                mail.To.Add("dee.snake@gmail.com");
                client.Send(mail);
            }
        }
    }
}
