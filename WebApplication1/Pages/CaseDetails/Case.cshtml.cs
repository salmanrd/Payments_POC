using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace WebApplication1.Pages.CaseDetails
{
    public class CaseModel : PageModel
    {
        public Case SelectedCase { get; set; }
        public void OnGet(string caseId)
        {
            var caseList = StaticData.CaseList;

            SelectedCase = caseList.FirstOrDefault(c => c.CaseId == caseId);
        }
            
    }
}
