namespace PaymentsAPI.Models
{
    public class Case
    {
        public Case()
        {
            ServiceRequests = new List<ServiceRequest>();
        }
        public string CaseId { get; set; }
        
        public List<ServiceRequest> ServiceRequests { get; set; }

    }
}



