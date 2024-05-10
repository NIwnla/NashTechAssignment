using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NashTechAssignmentDay8.Domain.Entities;

public class ProjectEmployee
{
    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public Guid EmployeeId { get; set; }

    [Required]
    public bool Enable { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual Project? Project{ get; set; }
}
