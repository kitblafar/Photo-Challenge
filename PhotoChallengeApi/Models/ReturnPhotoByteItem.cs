
namespace PhotoChallengeAPI.ViewModels
{
    public class ReturnPhotoByteItem(long id, string name, int challenge, byte[] image, bool? approved, string? message)
    {
        public long Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Challenge { get; set; } = challenge;
        public byte[] Image { get; set; } = image;

        public bool? Approved { get; set; } = approved;
        public string? Message { get; set; } = message;
    }
}
