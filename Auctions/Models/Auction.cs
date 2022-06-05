namespace Auctions.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
    }
}
