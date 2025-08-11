namespace PaymentsAPI.Models
{
    public class ServiceRequest
    {
        private int _invoiceAmount;
        public List<Fees> Fees { get; set; }
        public List<PaymentInstruction> Payments { get; set; }

        public bool CanPay
        {
            get
            {
                return Status.StartsWith("Partially Paid")|| Status.StartsWith( "Unpaid");
            }
        }

        public ServiceRequest()
        {
            Payments = new List<PaymentInstruction>();
            Fees = new List<Fees>();
        }
        public string Reference { get; set; }

        
        public int InvoiceAmount
        {
            get
            {
                _invoiceAmount = 0;
                foreach (var item in Fees)
                {
                    _invoiceAmount += item.ChargeableAmount;
                }
                return _invoiceAmount;
            }
        }

        public int InvoiceAmountGross
        {
            get
            {
                _invoiceAmount = 0;
                foreach (var item in Fees)
                {
                    _invoiceAmount += item.GrossAmount;
                }
                return _invoiceAmount;
            }
        }

        public string Status
        {

            get
            {
                return GetStatus();

                
            }
        }

        public int OverPayment
        {
            get
            {

                return this.Fees.Sum(x => x.OverPayment);

            }

        }

        public int AmountDue
        {
            get
            {

                return this.Fees.Sum(x => x.AmountDue);

            }

        }

        //public int OverPayment
        //{
        //    get
        //    {
        //        var _payments = 0;
        //        foreach (var item in Payments)
        //        {
        //            _payments += item.Amount;
        //        }
        //        if (_payments < InvoiceAmount)
        //        {
        //            return 0;
        //        }
        //        return _payments - InvoiceAmount   ;
        //    }
        //}
        //public int AmountDue
        //{
        //    get
        //    {
        //        var _payments = 0;
        //        foreach (var item in Payments)
        //        {
        //            _payments += item.Amount;
        //        }
        //        if (_payments >= InvoiceAmount)
        //        {
        //            return 0;
        //        }
        //        return InvoiceAmount - _payments;
        //    }
        //}



        internal  string GetStatus()
        {
            var _payments = 0;
            foreach (var item in Payments)
            {
                _payments += item.Amount;
            }

            if (_payments == InvoiceAmount)
            {
                return "Paid";
            }
            else if (_payments > InvoiceAmount)
            {
                return String.Concat("Overpaid by (" , _payments -  InvoiceAmount, ")");
            }
            else if (_payments > 0 && _payments < InvoiceAmount)
            {
                // return String.Concat("Partially Paid" , " Yet to pay :(",InvoiceAmount - _payments  ,")" );
                return String.Concat("Partially Paid", "");
            }
            else
            {
                return String.Concat("Unpaid" , " Yet to pay :(",InvoiceAmount - _payments  ,")");
            }
        }



    }
    
}



