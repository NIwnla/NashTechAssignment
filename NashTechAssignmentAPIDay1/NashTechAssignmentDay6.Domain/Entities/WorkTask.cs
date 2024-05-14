using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay6.Domain.Entities
{
	public class WorkTask
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public bool IsCompleted { get; set; } = default;
	}
}
