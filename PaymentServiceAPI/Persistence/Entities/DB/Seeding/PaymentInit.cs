using PaymentService.API.Persistence.Entities.DB.Models;

namespace PaymentService.API.Persistence.Entities.DB.Seeding;

public class PaymentInit
{
    public IList<Payment> GetPayments()
    {
        List<Payment> Payments = new List<Payment>();
        Payments.Add(new Payment
        {
            PaymentID = 1,
            //UserId = 1,
            //RoleId = 1,
            ReceiverBankNumber = "1234567890",
            GiverBankNumber = "0987654321",
            Price = 250,
            CreditAmount = 250,
            IsValid = true,
            TransactionType = "credit"
        });
        Payments.Add(new Payment
        {
            PaymentID = 2,
            //UserId = 2,
            //RoleId = 2, student
            ReceiverBankNumber = "3214569033",
            GiverBankNumber = "834234213",
            Price = 200,
            CreditAmount = 250,
            IsValid = true,
            TransactionType = "credit"
        });
        Payments.Add(new Payment
        {
            PaymentID = 3,
            //UserId = 3,
            //RoleId = 1,
            ReceiverBankNumber = "9029349234",
            GiverBankNumber = "3213933921",
            Price = 500,
            CreditAmount = 500,
            IsValid = true,
            TransactionType = "credit"
        });

        return Payments;
    }
}
