using System.ComponentModel.DataAnnotations.Schema;

namespace XmasApi.ViewModels
{
    public class XmasItemByte
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Challenge { get; set; }
        public byte[] Image { get; set; }
    }
}
