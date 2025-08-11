using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace PaymentsWeb.Pages.CaseDetails
{
    public class AddDiscountModel : PageModel
    {
        public ServiceRequest SelectedServiceRequest { get; set; }
        public string CaseId { get; set; }
        [BindProperty]
        public string FeeCode { get; set; }
        public int DiscountAmount { get; set; }

        private readonly StaticData _staticData;
        public AddDiscountModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        public void OnGet(string caseId, string sr, string fee)
        {
            CaseId = caseId;
            FeeCode = fee;
            var caseList = _staticData.CaseList;

            var case1 = caseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                SelectedServiceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
            }
        }


        public IActionResult OnPost(string caseId, string sr, string feeCode, int discountAmount)
        {

            if (discountAmount <= 0)
            {
                ModelState.AddModelError("DiscountAmount", "Discount amount must be greater than zero.");
                return Page();
            }

            if (discountAmount > 0)
            {
                var case1 = _staticData.CaseList.FirstOrDefault(c => c.CaseId == caseId);
                if (case1 != null)
                {
                    var serviceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
                    if (serviceRequest != null)
                    {
                        var fee = serviceRequest.Fees.FirstOrDefault(f => f.Code == feeCode);
                        if (fee != null && discountAmount > 0 && discountAmount > fee.GrossAmount)
                        {

                            ModelState.AddModelError("DiscountAmount", "Discount amount cant be greater Fee price.");
                            return Page();

                        }
                    }
                }
               
            }

            _staticData.AddDiscount(caseId, sr, feeCode, discountAmount);
            var caseRef = caseId;

            return RedirectToPage("/CaseDetails/Case", new { caseId = caseRef });


        }
    }
}
