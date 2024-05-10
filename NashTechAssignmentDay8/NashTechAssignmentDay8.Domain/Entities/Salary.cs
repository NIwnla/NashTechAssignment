using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NashTechAssignmentDay8.Domain.Entities;

public class Salary
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid EmployeeId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public virtual Employee? Employee { get; set; }
}
