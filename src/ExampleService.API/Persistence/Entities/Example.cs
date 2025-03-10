namespace ExampleService.API.Persistence;

public class Example
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public required string Name { get; set; }
    
    public string? Description { get; set; }
}