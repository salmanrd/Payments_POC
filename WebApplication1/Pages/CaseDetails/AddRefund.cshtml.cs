using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace PaymentsWeb.Pages.CaseDetails
{
    public class AddRefundModel : PageModel
    {
        private readonly StaticData _staticData;
        public AddRefundModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        public void OnGet(string caseId, string paymentReference)
        {
            var payment = _staticData.GetPaymentByReference(caseId, paymentReference);
            var fees = _staticData.GetFeesApportionedforPayment(payment);

            new RefundRequest().Create(payment, fees);


        }
    }
}
