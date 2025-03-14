using System;

namespace NotificationService.API.Persistence.Entities.DB.Interfaces;

public class Entity : IEntity<int>
{
    public int Id { get; set; }
}
