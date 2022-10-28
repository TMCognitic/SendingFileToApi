using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
#nullable disable
    public class FileContent
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
    }
#nullable disable
}
