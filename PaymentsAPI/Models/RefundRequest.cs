namespace PaymentsAPI.Models
{
    public class RefundRequest
    {
        public int Amount { get; set; }
        public string FeeId { get; set; }
        public static RefundRequest Create(PaymentInstruction payment, List<Fees> fees)
        {
            RefundRequest refundRequest = null;
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment), "Payment instruction cannot be null.");
            }

            if (fees.Count == 0)
            {
                throw new ArgumentNullException(nameof(fees), "Fees cannot be empty.");
            }

            foreach (var fee in fees)
            { 
                if (fee.OverPayment > 0)
                {
                    refundRequest = new RefundRequest
                    {
                        FeeId = fee.Id,
                        Amount = fee.OverPayment
                    };
                    break;
                }
            }

            return refundRequest;
        }


    }
}
