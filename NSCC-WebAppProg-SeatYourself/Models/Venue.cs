namespace NSCC_WebAppProg_SeatYourself.Models
{
    public class Venue
    {
        //Primary key
        public int VenueId { get; set; }


        //Attributes
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public string ImagePath { get; set; } = string.Empty; //Old name from before learning to upload
        public string? ImagePath { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }


        //Navigation properties
        public List<Occasion>? Occasions { get; set; }


    }
}
