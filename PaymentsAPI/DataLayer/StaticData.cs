using PaymentsAPI.Models;
using System.ComponentModel;

namespace PaymentsAPI.DataLayer
{
    public class StaticData
    {
        public List<Case> CaseList;
        public List<RefundInstruction> RefundList;


        public StaticData()
        {
            Init();
        }

        public void Init()
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
                               new Fees { Code = "F001", GrossAmount = 100 },
                               new Fees { Code = "F002", GrossAmount = 50 }
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
                    CreateApportionment(paymentAmount, serviceRequest, paymentInstruction);
                }
            }

        }

        private void CreateApportionment(int paymentAmount, ServiceRequest serviceRequest, PaymentInstruction paymentInstruction)
        {
            foreach (var fee in serviceRequest.Fees)
            {
                if (paymentAmount <= 0)
                    break;
                if (fee.AmountApportioned == fee.GrossAmount)
                    continue;

                var balance = fee.GrossAmount - fee.AmountApportioned;

               if( fee == serviceRequest.Fees.Last())
                {
                    fee.ApportionPayment(paymentInstruction, paymentAmount);
                    continue;
                }
               if (paymentAmount >= balance)
               {
                    fee.ApportionPayment(paymentInstruction, balance);
                    paymentAmount -= balance;
               }
                else
                {
                    fee.ApportionPayment(paymentInstruction, paymentAmount);
                    paymentAmount = 0;
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
                    if (fee != null && discountAmount > 0 && discountAmount <= fee.GrossAmount)
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

        public void AddServiceRequest(string caseId, FeeItem feeItem)
        {
            var case1 = CaseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                var newServiceRequest = new ServiceRequest
                {
                    Reference = "SR-" + DateTime.Now.Ticks,
                    Fees = new List<Fees>
                    {
                        new Fees { Code = feeItem.Code, GrossAmount = feeItem.Amount }
                    }
                };
                case1.ServiceRequests.Add(newServiceRequest);
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
                    Fees = selectedFees.Select(f => new Fees { Code = f.Code, GrossAmount = f.Amount }).ToList()
                };
                case1.ServiceRequests.Add(newServiceRequest);
            }
        }

        public List<Fees> GetFeesApportionedforPayment(PaymentInstruction paymentInstruction)
        {
            var apportionedFees = new List<Fees>();
            foreach (var case1 in CaseList)
            {
                foreach (var serviceRequest in case1.ServiceRequests)
                {
                    foreach (var fee in serviceRequest.Fees)
                    {
                        if (fee.ApportionmentList.Any(a => a.Payment.Reference == paymentInstruction.Reference))
                        {
                            apportionedFees.Add(fee);
                        }
                    }
                }
            }
            return apportionedFees;
        }

        public PaymentInstruction GetPaymentByReference(string caseId, string paymentReference)
        {
            PaymentInstruction payment = null;
            var case1 = CaseList.FirstOrDefault(c => c.CaseId == caseId);
            if (case1 != null)
            {
                foreach (var serviceRequest in case1.ServiceRequests)
                {
                    payment = serviceRequest.Payments.FirstOrDefault(p => p.Reference == paymentReference);
                    break;
                }
            }
            return payment;
        }

        public void AddRefund(string feeId, string paymentReference, int refundAmount)
        {
                var refundInstruction = new RefundInstruction
                {
                    Reference = "RF" + DateTime.Now.Ticks,
                    PaymentReference = paymentReference,
                    Amount = refundAmount,
                };
                if (RefundList == null)
                {
                    RefundList = new List<RefundInstruction>();
                }
                RefundList.Add(refundInstruction);

                var fee = CaseList.SelectMany(c => c.ServiceRequests)
                              .SelectMany(sr => sr.Fees)
                              .FirstOrDefault(f => f.Id == feeId);

                fee.AmountRefunded = refundAmount;
        }
            


        }
}
