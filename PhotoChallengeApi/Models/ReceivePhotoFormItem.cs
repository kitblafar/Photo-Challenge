using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoChallengeAPI.Models
{
    public class ReceivePhotoFormItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Challenge { get; set; }
        public IFormFile File { get; set; }
    }
}
