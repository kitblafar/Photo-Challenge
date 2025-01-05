namespace PhotoChallengeAPI.Models
{

    public class LeaderBoardSpecialItem(long id, long position, string name, long votes, List<string> voters)
    {
        public long Id {get; set;} = id;
        public long Position { get; set; } = position;
        public string Name { get; set; } = name;
        public long Votes { get; set; } = votes;
        public List<string> Voters { get; set; } = voters;
    }
}