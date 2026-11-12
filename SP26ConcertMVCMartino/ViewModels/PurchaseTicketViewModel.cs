namespace SP26ConcertMVCMartino.ViewModels
{
    public class PurchaseTicketViewModel
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public decimal Price { get; set; }

    }
}
