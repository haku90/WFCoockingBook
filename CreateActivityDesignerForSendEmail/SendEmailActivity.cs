using System.Activities;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace CreateActivityDesignerForSendEmail
{
    [Designer(typeof(SendEmailActivityDesigner))]
    public class SendEmailActivity : CodeActivity
    {
        public InArgument<string> From { get; set; }
        public InArgument<string> Host { get; set; }
        public InArgument<string> UserName { get; set; }
        public InArgument<string> Password { get; set; }
        public InArgument<string> To { get; set; }
        public InArgument<string> Subject { get; set; }
        public InArgument<string> Body { get; set; }
        public OutArgument<string> Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var mailMassage = new MailMessage();
            mailMassage.To.Add(To.Get(context));
            mailMassage.Subject = Subject.Get(context);
            mailMassage.Body = Body.Get(context);
            mailMassage.From = new MailAddress(From.Get(context));

            var smtp = new SmtpClient();
            smtp.Host = Host.Get(context);
            smtp.Credentials = new NetworkCredential(UserName.Get(context), Password.Get(context));
            smtp.EnableSsl = true;
            smtp.Send(mailMassage);
            Result.Set(context, "Send Email successfully!");
        }
    }
}