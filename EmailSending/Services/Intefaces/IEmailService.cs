
using EmailSending.Entities;

namespace EmailSending.Intefaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
