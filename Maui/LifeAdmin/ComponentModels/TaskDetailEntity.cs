using System.ComponentModel.DataAnnotations;
using SQLite;

namespace LifeAdmin.ComponentModels;

[Table("Tasks")]
public class TaskDetailEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Required]
    // [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
    public string? Description { get; set; }

    [Required]
    public DateTime CompletionDate { get; set; }
}