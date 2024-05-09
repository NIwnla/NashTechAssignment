using NashTechAssignmentDay7.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay7.Domain.Entities
{
	public class Person
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public GenderType Gender { get; set; }
		[Required]
		public string BirthPlace { get; set; }
	}
}
