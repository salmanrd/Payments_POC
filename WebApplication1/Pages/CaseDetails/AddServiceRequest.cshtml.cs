using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace PaymentsWeb.Pages.CaseDetails
{

    

    public class AddServiceRequestModel : PageModel
    {
        public List<FeeItem> AllFees { get; set; }
        [BindProperty]
        public string CaseId { get; set; }


        [BindProperty]
        public string FeeCode { get; set; }

        public int FeeAmount { get; set; }

        [BindProperty]
        public List<string> SelectedFeeIds { get; set; } = new();

        public List<FeeItem> SelectedFees { get; set; } = new();

        private readonly StaticData _staticData;
        public AddServiceRequestModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        private List<FeeItem> GetFeeList() => new()
        {
            new FeeItem { Code = "F001", Amount = 100 },
            new FeeItem { Code = "F002", Amount = 50 },
            new FeeItem { Code = "F003", Amount = 300 },
            new FeeItem { Code = "F004" , Amount = 273 },
            new FeeItem { Code = "F005" , Amount = 200 },
            new FeeItem { Code = "F006" , Amount = 45 },
            new FeeItem { Code = "F007" , Amount = 612 }
        };
        public void OnGet(string caseId)
        {
            AllFees = GetFeeList();
            CaseId = caseId;
        }

        public IActionResult OnPostAddFee(string caseId, string feeCode, int feeAmount)
        {
            AllFees = GetFeeList();
            var caseRef = caseId;
            if (feeAmount <= 0)
            {
                ModelState.AddModelError("FeeAmount", "Fee amount must be greater than zero.");
                return Page();
            }

            if (feeAmount > 0)
            {   
                
                _staticData.AddServiceRequest(caseId , new FeeItem { Code = feeCode, Amount = feeAmount });
            }
            return RedirectToPage("/CaseDetails/Case", new { caseId = caseRef });
        }

        public IActionResult OnPostAddFee1(string caseId)
        {
            AllFees = GetFeeList();
            SelectedFees = AllFees
                .Where(p => SelectedFeeIds.Contains(p.Code))
                .ToList();

            _staticData.AddServiceRequest(caseId, SelectedFees);

            var caseRef = caseId;

            return RedirectToPage("/CaseDetails/Case", new { caseId = caseRef });

        }
    }
}
