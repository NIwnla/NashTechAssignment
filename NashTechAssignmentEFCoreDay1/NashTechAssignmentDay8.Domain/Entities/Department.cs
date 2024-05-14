using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NashTechAssignmentDay8.Domain.Entities;

public class Department
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public virtual ICollection<Employee>? Employees { get; set; }
}
