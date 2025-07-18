using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace PaymentsWeb.Pages.CaseDetails
{

    

    public class AddServiceRequestModel : PageModel
    {
        public List<FeeItem> AllFees { get; set; }
        public string CaseId { get; set; }

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
            new FeeItem { Code = "F003", Amount = 80 },
            new FeeItem { Code = "F004" , Amount = 200 },
        };
        public void OnGet(string caseId)
        {
            AllFees = GetFeeList();
            CaseId = caseId;
        }

        public IActionResult OnPost(string caseId)
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
