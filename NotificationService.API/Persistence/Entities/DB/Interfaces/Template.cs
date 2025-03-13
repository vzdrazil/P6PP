using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.API.Persistence.Entities.DB.Interfaces;

public class Template
{
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(75)]
    public string? Subject { get; set; }
    //Budou vytvoreny zastupne znaky pro vlozeni promennych(jmeno,datum,atd.)
    [StringLength(1500)]
    public required string Text { get; set; }
    [ForeignKey(nameof(TemplateType))]
    public required int TypeId { get; set; }
}