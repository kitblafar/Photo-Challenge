using System.ComponentModel.DataAnnotations.Schema;

namespace XmasApi.Models
{

    public class XmasItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Challenge { get; set; }
        public string FilePath { get; set; }

    }
}
