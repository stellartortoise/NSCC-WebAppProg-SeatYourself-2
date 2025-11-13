namespace NSCC_WebAppProg_SeatYourself.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string NumTicketsOrdered { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string CreditCardNumber { get; set; } = string.Empty;
        public string CreditCardExpiry { get; set; } = string.Empty;
        public int CreditCardCvv { get; set; }

        public DateTime PurchaseDate { get; set; }

        // foreign key
        public int OccasionId { get; set; }

        // navigation property
        public Occasion? Occasion { get; set; }
    }
}
