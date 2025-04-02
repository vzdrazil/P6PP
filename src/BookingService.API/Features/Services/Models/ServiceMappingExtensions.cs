using BookingService.API.Domain.Models;
using BookingService.API.Features.Bookings.Models;

namespace BookingService.API.Features.Services.Models;

public static class ServiceMappingExtensions
{
    public static Service Map(this CreateServiceRequest input)
    => new()
    {
        Start = input.Start,
        End = input.End,
        Price = input.Price,
        ServiceName = input.ServiceName,
        TrainerId = input.TrainerId,
        RoomId = input.RoomId,
    };

    public static ServiceResponse Map(this Service input)
        => new(
                input.Id,
                input.Start,
                input.End, input.Price,
                input.ServiceName!,
                input.Users.Count,
                input.Room!.Capacity,
                input.Room.Name!);

    public static IList<ServiceResponse> Map(this IList<Service> input)
        => [.. input.Select(Map)];
}
