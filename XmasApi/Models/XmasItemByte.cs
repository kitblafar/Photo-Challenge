
namespace XmasAPI.ViewModels
{
    public class XmasItemByte
    {
        public XmasItemByte(long id, string name, int challenge, byte[] image)
        {
            Id = id;
            Name = name;
            Challenge = challenge;
            Image = image;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Challenge { get; set; }
        public byte[] Image { get; set; }
    }
}
