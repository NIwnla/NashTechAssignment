using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NashTechAssignmentDay8.Domain.Entities;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public Guid DepartmentId { get; set; }

    [Required]
    public DateTime JoinedDate { get; set; }
    public virtual Salary? Salary { get; set; }
    public virtual Department? Department { get; set; }
    public virtual ICollection<Project>? Projects { get; set; }
}
