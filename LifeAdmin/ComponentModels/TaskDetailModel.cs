using System.ComponentModel.DataAnnotations;

namespace LifeAdmin.ComponentModels;

public class TaskDetailModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    // [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
    public string? Description { get; set; }

    [Required]
    public DateTime CompletionDate { get; set; }
}