namespace PaymentsAPI.Models
{
    public class OverPaidRefundItem
    {
        public int Amount { get; set; }
        public string RefundReference { get; set; }
    }
    public class Fees
    {
        public Fees()
        {
            Id = Guid.NewGuid().ToString();
            Remissiom = new HelpWithFees();
            
            _apportionmentList = new List<Apportionment>();
            _overPaidRefundItemList = new List<OverPaidRefundItem>();
        }

        public string Id 
        {
            get;
        }
        private List<OverPaidRefundItem> _overPaidRefundItemList;
        public List<OverPaidRefundItem> OverPaidRefundItemList
        {
            get { return _overPaidRefundItemList; }
        }

        private List<Apportionment> _apportionmentList;
        public List<Apportionment> ApportionmentList
        {
            get { return _apportionmentList; }
        }

        public  int AmountRefunded 
        { 
            get 
            { 
                return _overPaidRefundItemList.Sum(x => x.Amount); 
            }
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
                return AmountApportioned - ChargeableAmount - AmountRefunded > 0 ? 
                    AmountApportioned - ChargeableAmount - AmountRefunded: 0;
            }
        }

        public int AmountDue
        {
            get
            {
                return ChargeableAmount - AmountApportioned > 0 ? ChargeableAmount - AmountApportioned: 0;
            }
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



