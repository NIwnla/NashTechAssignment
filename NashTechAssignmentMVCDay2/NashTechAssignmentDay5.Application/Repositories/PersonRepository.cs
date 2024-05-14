using NashTechAssignmentDay5.Application.Interfaces;
using NashTechAssignmentDay5.Domain.Entities;
using NashTechAssignmentDay5.Infrastructure.Extensions;

namespace NashTechAssignmentDay5.Application.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private List<Person> People;
		public PersonRepository()
		{
			People = new List<Person>().GetDataFromFile();
		}

		public bool Create(Person person)
		{
			People.Add(person);
			return People.SaveDataToFile();
		}

		public bool Delete(Person person)
		{
			People.Remove(person);
			return People.SaveDataToFile();
		}

		public List<Person> FindByCondition(Func<Person, bool> condition)
		{
			return People.Where(condition).ToList();
		}

		public List<Person> GetAll()
		{
			return People;
		}

		public bool Update(Person updatedPerson)
		{
			var personToUpdate = People.Find(p => p.Id == updatedPerson.Id);
			if (personToUpdate != null)
			{
				personToUpdate.FirstName = updatedPerson.FirstName;
				personToUpdate.LastName = updatedPerson.LastName;
				personToUpdate.PhoneNumber = updatedPerson.PhoneNumber;
				personToUpdate.BirthPlace = updatedPerson.BirthPlace;
				personToUpdate.DateOfBirth = updatedPerson.DateOfBirth;
				personToUpdate.IsGraduated = updatedPerson.IsGraduated;
				personToUpdate.Gender = updatedPerson.Gender;
				return People.SaveDataToFile();
			}
			return false;
		}
	}
}
