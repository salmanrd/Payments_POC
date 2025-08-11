namespace PaymentsAPI.Models
{
    public class Fees
    {
        private List<Apportionment> _apportionmentList;
        public List<Apportionment> ApportionmentList
        {
            get { return _apportionmentList; }
        }
        public bool CanAddRemission
        {
            get
            {
                return Remissiom != null  && Remissiom.Discount == 0;
            }
        }

        public int OverPayment
        {
            get
            {
                return AmountApportioned - ChargeableAmount > 0 ? AmountApportioned - ChargeableAmount : 0;
            }
        }

        public int AmountDue
        {
            get
            {
                return ChargeableAmount - AmountApportioned > 0 ? ChargeableAmount - AmountApportioned: 0;
            }
        }


        public Fees()
        {
            Remissiom = new HelpWithFees();
            _apportionmentList = new List<Apportionment>();
        }

        public int AmountApportioned

        { 
            get 
            { 
                return _apportionmentList.Sum(x => x.ApportionedAmount); 
            } 
        }
        public string Code { get; set; }
        public int GrossAmount { get; set; }

        public HelpWithFees Remissiom { get; set; }

        public int ChargeableAmount
        {
            get
            {
                return GrossAmount - Remissiom.Discount;
            }
        }

        public void ApportionPayment(PaymentInstruction payment, int amount)
        {   
            var _apportionment = new Apportionment
            {
                Payment = payment,
                ApportionedAmount = amount
            };
            ApportionmentList.Add(_apportionment);
        }


    }
}



