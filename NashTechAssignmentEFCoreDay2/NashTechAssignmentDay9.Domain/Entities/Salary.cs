using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NashTechAssignmentDay9.Domain.Entities;

public class Salary
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid EmployeeId { get; set; }

    [Required]
    public double Amount { get; set; }

    public virtual Employee? Employee { get; set; }
}
