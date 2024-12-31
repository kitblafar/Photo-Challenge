
namespace PhotoChallengeAPI.ViewModels
{
    public class ReturnPhotoSpecialByteItem(long id, string name, int challenge, byte[] image, string[] voters, long votes)
    {
        public long Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Challenge { get; set; } = challenge;
        public byte[] Image { get; set; } = image;

        public string[] Voters {get;set;} = voters;
        public long Votes {get;set;} = votes;

    }
}
