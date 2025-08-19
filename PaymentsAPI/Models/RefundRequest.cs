namespace PaymentsAPI.Models
{
    public class RefundRequest
    {

        public void Create(PaymentInstruction payment, List<Fees> fees)
        {
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
                    
                }
            }













            // Here you would typically add logic to process the refund request
            // For example, you might call a payment gateway API to initiate the refund
            // This is a placeholder for such logic
            Console.WriteLine($"Refund requested for payment with reference: {payment.Reference}, amount: {payment.Amount}");

        }


    }
}
