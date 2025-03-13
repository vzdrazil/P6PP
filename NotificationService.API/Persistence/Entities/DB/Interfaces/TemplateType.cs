using System.ComponentModel.DataAnnotations;
namespace NotificationService.API.Persistence.Entities.DB.Interfaces;

public class TemplateType
{
    [StringLength(50)]
    public required string Name { get; set; }
}