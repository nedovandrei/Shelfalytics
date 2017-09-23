using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using System.Web.Hosting;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;

namespace Shelfalytics.Service
{
    public class MailService : IMailService
    {
        private readonly IEquipmentDataRepository _equipmentDataRepository;
        private readonly IMailQueueRepository _mailQueueRepository;

        private readonly int _timespanForDeletion = 1;

        public MailService(IEquipmentDataRepository equipmentDataRepository, IMailQueueRepository mailQueueRepository)
        {
            if (equipmentDataRepository == null)
            {
                throw new ArgumentNullException(nameof(equipmentDataRepository));
            }

            if (mailQueueRepository == null)
            {
                throw new ArgumentNullException(nameof(mailQueueRepository));
            }

            _equipmentDataRepository = equipmentDataRepository;
            _mailQueueRepository = mailQueueRepository;
        }
        public async Task SendOOSEmail(ProductOOSDTO product, int equipmentId)
        {
            var users = await _equipmentDataRepository.GetEquipmentUsers(equipmentId);

            var mailQueue = await _mailQueueRepository.GetProductMailQueue(equipmentId, product.ProductId);

            var emailTemplate = File.ReadAllText(HostingEnvironment.MapPath("~/Content/MessageTemplates/OOSNotification.html"));

            var mail = new MailMessage
            {
                From = new MailAddress("notify@shelf.work"),
                Subject = "Out Of Stock Notification",
                IsBodyHtml = true,
                Body = emailTemplate
            };
            foreach(var user in users)
            {
                if (!(mailQueue.Any(x => x.UserId == user.Id) && mailQueue.Any(x => x.ProductId == product.ProductId) && mailQueue.Any(x => x.EquipmentId == product.EquipmentId)))
                {
                    mail.To.Add(new MailAddress(user.Email));
                }
            }

            if (mail.To.Count() == 0)
            {
                return;
            }

            using (var client = new SmtpClient("mail.shelf.work"))
            {
                
                client.Port = 8889;

                var credentials = new NetworkCredential("notify@shelf.work", "Shelf3592!");
                client.Credentials = credentials;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = "mail.shelf.work";


                
                foreach (var user in users)
                {
                    mail.Body = emailTemplate;
                    mail.Body = mail.Body.Replace("%SKUName%", product.SKUName);
                    mail.Body = mail.Body.Replace("%Price%", product.Price.ToString());
                    mail.Body = mail.Body.Replace("%TimeStamp%", product.TimeStamp.ToString());
                    mail.Body = mail.Body.Replace("%PhotoPath%", product.PhotoPath);
                    mail.Body = mail.Body.Replace("%FirstName%", user.EmployeeName);

                    mail.To.Clear();
                    mail.To.Add(new MailAddress(user.Email));
                    try
                    {
                        client.Send(mail);
                        await _mailQueueRepository.AddToQueue(new MailOosQueueDTO
                        {
                            ClientId = user.ClientId,
                            EquipmentId = product.EquipmentId,
                            ProductId = product.ProductId,
                            UserId = user.Id
                        });
                    }
                    catch (Exception ex)
                    {
                        ExceptionCatcher(ex);
                    }
                }
                    
                
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

        private async Task DeleteOutdatedQueues()
        {
            await _mailQueueRepository.DeleteOutdatedQueues(_timespanForDeletion);
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
