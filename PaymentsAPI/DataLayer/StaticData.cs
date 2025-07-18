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

        public void AddDiscount(string caseId, string sr, string feeCode, int discountAmount)
        {
            var case1 = CaseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                var serviceRequest = case1.ServiceRequests.FirstOrDefault(s => s.Reference == sr);
                if (serviceRequest != null)
                {
                    var fee = serviceRequest.Fees.FirstOrDefault(f => f.Code == feeCode);
                    if (fee != null && discountAmount > 0 && discountAmount <= fee.Amount)
                    {

                        fee.Remissiom = new HelpWithFees
                        {
                            Reference = "HWF-" + DateTime.Now.Ticks,
                            Discount = discountAmount,
                        };
                        
                    }
                }
            }

        }


        public void AddServiceRequest(string caseId, List<FeeItem> selectedFees)
        {
            var case1 = CaseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                var newServiceRequest = new ServiceRequest
                {
                    Reference = "SR-" + DateTime.Now.Ticks,
                    Fees = selectedFees.Select(f => new Fees { Code = f.Code, Amount = f.Amount }).ToList()
                };
                case1.ServiceRequests.Add(newServiceRequest);
            }
        }
    }
}
