namespace Data
{
	public class Student
	{
		public string? FirstName {  get; set; }
		public string? LastName { get; set; }
		public bool? Gender { get; set; }
		public DateTime? DOB { get; set; }
		public string? PhoneNumber { get; set; }
		public string? BirthPlace { get; set; }
		public int? Age { get => DateTime.Now.Year - DOB.Value.Year;}
		public bool? IsGraduated { get; set; }
	}
}
