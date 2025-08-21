namespace PaymentsAPI.Models
{
    public class RefundInstruction
    {
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string PaymentReference { get; set; }
        public string FeeId { get; set; }

        public string Status { get { return "Approved"; } }

    }
}
