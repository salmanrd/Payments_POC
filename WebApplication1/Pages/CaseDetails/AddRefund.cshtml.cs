using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace PaymentsWeb.Pages.CaseDetails
{
    public class AddRefundModel : PageModel
    {
        private readonly StaticData _staticData;

        [BindProperty]
        public string FeeId { get; set; }

        [BindProperty]
        public string PaymentReference { get; set; }
        public bool CanAddRefund { get; set; }
        public int RefundAmount { get; set; }
        public AddRefundModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        public void OnGet(string caseId, string paymentReference)
        {
            PaymentReference = paymentReference;
            var payment = _staticData.GetPaymentByReference(caseId, paymentReference);
            var fees = _staticData.GetFeesApportionedforPayment(payment);

            var request = RefundRequest.Create(payment, fees);
            if (request != null)
            {
                FeeId = request.FeeId;
                RefundAmount = request.Amount;
            }
            CanAddRefund = request != null;


        }

        public IActionResult OnPost(string paymentReference, string feeId, int refundAmount)
        {
            if (refundAmount <= 0)
            {
                ModelState.AddModelError("RefundAmount", "Refund amount must be greater than zero.");
                return Page();
            }
            _staticData.AddRefund(feeId, paymentReference, refundAmount);

            return RedirectToPage("/CaseList");
        }
    }
}
