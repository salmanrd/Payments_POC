using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsAPI.DataLayer;
using PaymentsAPI.Models;

namespace WebApplication1.Pages
{
    public class CaseListModel : PageModel
    {
        public List<Case> CaseList { get; set; }

        private readonly StaticData _staticData;
        public CaseListModel(StaticData staticData)
        {
            _staticData = staticData;
        }
       
        public void OnGet()
        {
            
            CaseList = _staticData.CaseList;

        }
    }
}
