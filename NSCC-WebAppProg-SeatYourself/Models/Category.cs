namespace NSCC_WebAppProg_SeatYourself.Models
{
    public class Category
    {
        //Primary key
        public int CategoryId { get; set; }
        
        
        //Attributes
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        //Navigation properties
        public List<Occasion>? Occasions { get; set; }
    }
}
