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
                return Status == "Partially Paid" || Status == "Unpaid";
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

        public string Status
        {

            get
            {
                return GetStatus();

                
            }
        }

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
                return "Overpaid";
            }
            else if (_payments > 0 && _payments < InvoiceAmount)
            {
                return "Partially Paid";
            }
            else
            {
                return "Unpaid";
            }
        }



    }
    
}



