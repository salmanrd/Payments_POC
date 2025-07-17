using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace WebApplication1.Pages
{
    public class CaseListModel : PageModel
    {
        public List<Case> CaseList { get; set; }

        public void OnGet()
        {
            StaticData.InitialiseData();
            CaseList = StaticData.CaseList;
            
        }
    }
}
