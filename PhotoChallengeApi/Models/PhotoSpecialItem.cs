using System.ComponentModel.DataAnnotations;

namespace PhotoChallengeAPI.Models
{

    public class PhotoSpecialItem
    {
        public long Id { get; set; }
        public  string Name { get; set; }
        public int Challenge { get; set; }
        public  string FilePath { get; set; }

        public string[] Voters {get;set;}
        public long Votes {get;set;}

    }
}
