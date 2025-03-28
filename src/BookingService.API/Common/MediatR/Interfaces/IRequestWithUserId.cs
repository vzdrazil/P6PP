namespace BookingPayments.API.Common.MediatR.Interfaces;

public interface IRequestWithUserId
{
    int UserId { get; set; }
}
