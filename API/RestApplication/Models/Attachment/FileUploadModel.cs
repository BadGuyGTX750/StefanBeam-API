using System.Reflection;

namespace RestApplication.Models.Attachment
{
    public class FileUploadModel
    {
        public string name { get; set; }
        
        public string productName { get; set; }

        public IFormFile content { get; set; }
    }
}
