namespace BookingPayments.API.Entities.Seeding;

internal class ServicesInit
{
    public IList<Services> GetServices()
    {
        return new List<Services>
        {
            new Services { Id = 1, TrainerId = 1, Price = 100, ServiceName = "Service A", IsCancelled = false },
            new Services { Id = 2, TrainerId = 2, Price = 200, ServiceName = "Service B", IsCancelled = false }
        };
    }
}