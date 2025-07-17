namespace PaymentsAPI.Models
{
    public class Fees
    {
        public Fees()
        {
            Remissiom = new HelpWithFees();
        }
        public string Code { get; set; }
        public int Amount { get; set; }

        public HelpWithFees Remissiom { get; set; }

        public int ChargeableAmount
        {
            get
            {
                return Amount - Remissiom.Discount;
            }
        }

    }
}



