using System.ComponentModel.DataAnnotations;

namespace WebShell.Domain
{
    public class ShellRequest: BaseEntity
    {
        [Required]
        public string Input { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
    }
}
