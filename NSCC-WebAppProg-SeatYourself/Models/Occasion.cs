using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace NSCC_WebAppProg_SeatYourself.Models
{
    public class Occasion
    {
        //Primary key
        public int OccasionId { get; set; }


        //Attributes
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Owner { get; set; } = string.Empty;
        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }
        public float Price { get; set; }

        // Image Filename
        public string? Filename { get; set; }


        //Foreign keys
        [Display(Name = "Venue")]
        public int VenueId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Comment")]
        public int? CommentId { get; set; }
        [Display(Name = "Purchase")]
        public int? PurchaseId { get; set; }



        //Navigation properties
        public Venue? Venue { get; set; }
        public Category? Category { get; set; }

        public List<Comment>? Comments { get; set; }
        public List<Purchase>? Purchases { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        [Required(ErrorMessage = "An image is required.")]
        public IFormFile? ImageFile { get; set; }
    }
}
