using NashTechAssignmentDay4.Models;

namespace NashTechAssignmentDay5.Interfaces
{
	public interface IRookiesService
	{
		List<Person> GetPeople();
		Person GetPersonById(int id);
		bool EditPerson(Person updatedPerson);
		bool DeletePerson(int id);
		bool AddPerson(Person addedPerson);
		List<Person> GetMales();
		Person GetOldest();
		List<Person> GetByBirthYear(int input);
		bool ExportToExcel(string? path);
	}
}
