using PaymentsAPI.Models;

namespace PaymentsAPI.DataLayer
{
    public static class StaticData
    {
        public static List<Case> CaseList { get; set; }

        public static void InitialiseData()
        {
            CaseList = new List<Case>();

            CaseList = new List<Case>
            {
                new Case { CaseId = "101", ServiceRequests = new List<ServiceRequest>
                    {
                       new ServiceRequest
                       {
                           Reference = "SR101",
                           Fees = new List<Fees>
                           {
                               new Fees { Code = "F001", Amount = 100 },
                               new Fees { Code = "F002", Amount = 50 }
                           }
                       }
                    }
                }

            };

        }
    }
}
