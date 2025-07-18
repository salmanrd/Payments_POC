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

        private readonly StaticData _staticData;
        public AddPaymentModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        public void OnGet(string caseId, string sr)
        {
            var caseList = _staticData.CaseList;

            var case1 = caseList.FirstOrDefault(c => c.CaseId == caseId);
            CaseId = caseId;
            if (case1 != null)
            {
                SelectedServiceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
            }
        }

        public IActionResult OnPost(int PaymentAmount, string caseId, string sr)
        {

            if (PaymentAmount <= 0)
            {
                ModelState.AddModelError("PaymentAmount", "Payment amount must be greater than zero.");
                return Page();
            }

            _staticData.AddPayment(caseId, sr, PaymentAmount);
            var caseRef = caseId;

            return RedirectToPage("/CaseDetails/Case", new { caseId = caseRef });
            
            
        }
    }
}
