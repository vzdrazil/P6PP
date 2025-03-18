namespace BookingPayments.API.Entities.Seeding;

internal class DiscountsInit
{
    public IList<Discounts> GetDiscounts()
    {
        return new List<Discounts>
        {
            new Discounts { Id = 1, DiscountPercentage = 10, ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddMonths(1), IsValid = true },
            new Discounts { Id = 2, DiscountPercentage = 20, ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddMonths(1), IsValid = true }
        };
    }
}