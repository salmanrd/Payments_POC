using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;

namespace PaymentsWeb.Pages.CaseDetails
{
    public class ResetModel : PageModel
    {

        private readonly StaticData _staticData;
        

        public ResetModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        public void OnGet()
        {
            _staticData.Init();
            TempData["Message"] = "Data has been reset to initial state.";
            RedirectToPage("/Index");

        }
    }
}
