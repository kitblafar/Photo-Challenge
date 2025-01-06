using System.ComponentModel.DataAnnotations;

namespace PhotoChallengeAPI.Models
{

    public class AuthenticateItem
    {
        public long Id {get; set;}

        public required string Name {get; set;}
        public required string Value {get; set;}
    }
}