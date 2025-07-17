namespace PaymentsAPI.Models
{
    public class PaymentInstruction
    {
        public string Reference { get; set; }
        public string PaymentMethod { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; } = "Success";
        
    }
    
}



