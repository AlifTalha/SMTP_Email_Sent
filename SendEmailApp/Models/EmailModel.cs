using Microsoft.AspNetCore.Http;

namespace SendEmailApp.Models
{
    public class EmailModel
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public IFormFile? Attachment { get; set; } // optional file
    }
}
