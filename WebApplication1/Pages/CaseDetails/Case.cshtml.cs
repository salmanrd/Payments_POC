using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace WebApplication1.Pages.CaseDetails
{
    public class CaseModel : PageModel
    {
        private readonly StaticData _staticData;
        public Case SelectedCase { get; set; }

        public CaseModel(StaticData staticData)
        {
             _staticData = staticData;
        }
        public void OnGet(string caseId)
        {
            var caseList = _staticData.CaseList ;

            SelectedCase = caseList.FirstOrDefault(c => c.CaseId == caseId);
        }
            
    }
}
