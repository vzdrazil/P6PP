using BookingPayments.API.Entities;

namespace BookingPayments.API.Data.Seeding;

internal class ServicesInit
{
    public static IEnumerable<Service> GetServices()
    {
        return
        [
            new Service
            {
                Id = 1,
                TrainerId = 1,
                Price = 100,
                ServiceName = "Service A",
                IsCancelled = false
            },
            new Service
            {
                Id = 2,
                TrainerId = 2,
                Price = 200,
                ServiceName = "Service B",
                IsCancelled = false
            }
        ];
    }
}