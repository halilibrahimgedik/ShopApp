namespace ShopApp.WebUI.EmailServices
{
    public interface IEmailSender
    {
        //! smtp => hosting aldığımızda email server veriyor (smtp ayarlarını firmaya sorabiliriz), gmail, hotmail kullanılabilir
        //! api => sendgrid

        Task SendEmailAsync(string email, string subject, string htmlMessage);

    }
}
