using NashTechAssignmentDay5.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay5.Domain.Entities
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
		public GenderType Gender { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
		public string PhoneNumber { get; set; }
		[Required]
		public string BirthPlace { get; set; }
		public bool IsGraduated { get; set; } = default;
	}
}
