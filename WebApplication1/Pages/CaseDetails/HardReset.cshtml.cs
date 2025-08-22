using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;

namespace PaymentsWeb.Pages.CaseDetails
{
    public class HardResetModel : PageModel
    {

        private readonly StaticData _staticData;
        

        public HardResetModel(StaticData staticData)
        {
            _staticData = staticData;
        }
        public void OnGet()
        {
            _staticData.HardReset();
            TempData["Message"] = "Data has been hard reset ";
            RedirectToPage("/Index");

        }
    }
}
