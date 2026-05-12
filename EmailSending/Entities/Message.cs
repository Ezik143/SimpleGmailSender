using MimeKit;

namespace EmailSending.Entities
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            foreach (var address in to)
            {
                To.Add(MailboxAddress.Parse(address));
            }
            Subject = subject;
            Content = content;
        }
    }
}
