using System.Text.Json.Serialization;

namespace NashTechAssignmentDay9.Application.Common.Models;

public class EmployeeDto
{
    public string DepartmentName { get; set; }
    public string Name { get; set; }
    public DateTime JoinedDate { get; set; }
}
