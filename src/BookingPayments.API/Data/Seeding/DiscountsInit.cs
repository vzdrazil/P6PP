using BookingPayments.API.Entities;

namespace BookingPayments.API.Data.Seeding;

internal class DiscountsInit
{
    public static IEnumerable<Discount> GetDiscounts()
    {
        return
        [
            new Discount
            {
                Id = 1,
                Percentage = 10,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddMonths(1),
                IsValid = true
            },
            new Discount
            {
                Id = 2,
                Percentage = 20,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddMonths(1),
                IsValid = true
            }
        ];
    }
}