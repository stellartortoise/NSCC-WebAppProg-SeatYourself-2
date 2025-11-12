namespace NSCC_WebAppProg_SeatYourself.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreateDatet { get; set; }

        // foreign key
        public int OccasionId { get; set; }

        // navigation property
        public Occasion? Occasion { get; set; }
    }
}
