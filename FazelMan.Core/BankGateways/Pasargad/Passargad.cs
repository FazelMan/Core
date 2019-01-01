namespace FazelMan.Core.BankGateways.Pasargad
{
    public class Passargad
    {
        public int MerchantCode { get; set; }
        public int TerminalCode { get; set; }
        public int Amount { get; set; }
        public string RedirectAddress { get; set; }
        public string TimeStamp { get; set; }
        public string InvoiceDate { get; set; }
        public string Action { get; set; }
        public string Sign { get; set; }
        public long TransactionId { get; set; }
    }

}
