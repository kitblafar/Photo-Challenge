using System.ComponentModel.DataAnnotations;

namespace XmasAPI.Models
{

    public class XmasItem
    {
        public long Id { get; set; }
        public  string Name { get; set; }
        public int Challenge { get; set; }
        public  string FilePath { get; set; }
        public bool? Approved { get; set; }
        public string? Message { get; set; }

    }
}
