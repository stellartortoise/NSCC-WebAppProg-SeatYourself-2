﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        // Image Filename
        public string? Filename { get; set; }


        //Foreign keys
        [Display(Name = "Venue")]
        public int VenueId { get; set; }
        [Display(Name = "Occasion")]
        public int CategoryId { get; set; }
        

        //Navigation properties
        public Venue? Venue { get; set; }
        public Category? Category { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
