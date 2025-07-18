namespace PaymentsAPI.Models
{
    public class FeeItem
    {
        public string Code { get; set; }
        public int Amount { get; set; }
    }
    public class Fees
    {
        public bool CanAddRemission
        {
            get
            {
                return Remissiom != null  && Remissiom.Discount == 0;
            }
        }
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



