using NashTechAssignmentDay7.Application.Common.Interfaces;
using NashTechAssignmentDay7.Domain.Entities;
using NashTechAssignmentDay7.Infrastructure.Data;

namespace NashTechAssignmentDay7.Application.Repositories
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

		public bool CreateRange(IEnumerable<Person> people)
		{
			People.AddRange(people);
			return People.SaveDataToFile();
		}

		public bool Delete(Person person)
		{
			var personToDelete = People.Find(w => w.Id == person.Id);
			if(personToDelete == null)
			{
				return false;
			}
			People.Remove(personToDelete);
			return People.SaveDataToFile();
		}

		public IEnumerable<Person> FindByCondition(Func<Person, bool> condition)
		{
			return People.Where(condition);
		}

		public IEnumerable<Person> GetPeople()
		{
			return People;
		}

		public bool Update(Person person)
		{
			var personToUpdate = People.Find(w => w.Id == person.Id);
			if(personToUpdate == null)
			{
				return false;
			}
			personToUpdate.FirstName = person.FirstName;
			personToUpdate.LastName = person.LastName;
			personToUpdate.DateOfBirth = person.DateOfBirth;
			personToUpdate.BirthPlace = person.BirthPlace;
			personToUpdate.Gender = person.Gender;
			
			return People.SaveDataToFile();
		}
	}
}
