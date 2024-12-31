namespace PhotoChallengeAPI.Models
{

    public class LeaderBoardItem(long position, string name, long challengeCount)
    {
        public long Position { get; set; } = position;
        public string Name { get; set; } = name;
        public long ChallengeCount { get; set; } = challengeCount;

    }
}