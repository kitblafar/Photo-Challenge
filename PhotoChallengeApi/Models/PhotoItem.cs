using System.ComponentModel.DataAnnotations;

namespace PhotoChallengeAPI.Models
{

    public class PhotoItem
    {
        public long Id { get; set; }
        public  string Name { get; set; }
        public int Challenge { get; set; }
        public  string FilePath { get; set; }
        public bool? Approved { get; set; }
        public string? Message { get; set; }

    }
}
