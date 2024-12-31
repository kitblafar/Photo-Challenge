namespace PhotoChallengeAPI.Models
{

    public class ApprovalItem
    {
        public long Id { get; set; }
        public bool? Approved { get; set; }
        public string? Message { get; set; }

    }
}