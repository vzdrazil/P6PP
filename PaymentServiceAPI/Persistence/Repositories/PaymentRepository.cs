
using Microsoft.EntityFrameworkCore;
using PaymentService.API.Persistence.Entities.DB.Models;

namespace PaymentService.API.Persistence.Repositories
{
    public class PaymentRepository
    {
        private readonly DapperContext _context;
        public PaymentRepository(DapperContext context)
        {
            _context = context;
        }
        internal async Task<int?> AddAsync(Payment payment, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal async Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = await _context.CreateConnectionAsync();
            const string query = @"
        SELECT PaymentID, UserId,RoleId,ReceiverBankNumber,GiverBankNumber,Price,CreditAmount,IsValid,TransactionType
        FROM Payment
        WHERE UserId = @Id;";

            return await connection.QueryFirstOrDefaultAsync<Payment>(query, new { Id = id });
        }
    }
}
