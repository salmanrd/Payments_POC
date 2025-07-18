using PaymentsAPI.Models;

namespace PaymentsAPI.DataLayer
{
    public class StaticData
    {
        public List<Case> CaseList;
        
        public StaticData()
        {
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

        public void AddPayment(string caseId, string sr, int paymentAmount)
        {
            var case1 = CaseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                var serviceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
                if (serviceRequest != null && serviceRequest.CanPay)
                {
                    var paymentInstruction = new PaymentInstruction
                    {
                        Reference = "PAY" + DateTime.Now.Ticks,
                        PaymentMethod = "Online",
                        Amount = paymentAmount,
                        Status = "Success"
                    };
                    serviceRequest.Payments.Add(paymentInstruction);
                }
            }

        }
    }
}
