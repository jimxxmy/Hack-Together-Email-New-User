using System.ComponentModel.DataAnnotations;

namespace NETCoreMVCwithMSGraph.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class SubScribeModel
    {
        [Required]
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
    }
}