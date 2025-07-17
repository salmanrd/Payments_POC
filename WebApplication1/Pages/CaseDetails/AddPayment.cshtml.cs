using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace WebApplication1.Pages.CaseDetails
{


    public class AddPaymentModel : PageModel
    {
        public ServiceRequest SelectedServiceRequest { get; set; }
        public string CaseId { get; set; }
        public int PaymentAmount { get; set; }
        public void OnGet(string caseId, string sr)
        {
            var caseList = StaticData.CaseList;

            var case1 = caseList.FirstOrDefault(c => c.CaseId == caseId);
            CaseId = caseId;
            if (case1 != null)
            {
                SelectedServiceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
            }
        }

        public void OnPost(int PaymentAmount, string caseId, string sr)
        {
            var caseList = StaticData.CaseList;
            var case1 = caseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                var serviceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
                if (serviceRequest != null && serviceRequest.CanPay)
                {
                    var paymentInstruction = new PaymentInstruction
                    {
                        Reference = "PAY" + DateTime.Now.Ticks,
                        PaymentMethod = "Online",
                        Amount = PaymentAmount,
                        Status = "Success"
                    };
                    serviceRequest.Payments.Add(paymentInstruction);
                }
            }
        }
    }
}
