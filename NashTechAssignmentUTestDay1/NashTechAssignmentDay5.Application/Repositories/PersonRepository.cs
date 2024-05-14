using NashTechAssignmentDay5.Application.Interfaces;
using NashTechAssignmentDay5.Domain.Entities;
using NashTechAssignmentDay5.Domain.Enum;
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

		public List<Person> FindByBirthYear(int input)
		{
			switch (input)
			{
				case 1:
					return People.Where(p => p.DateOfBirth.Year > 2000).ToList();
				case 2:
					return People.Where(p => p.DateOfBirth.Year < 2000).ToList();
				default:
					return People.Where(p => p.DateOfBirth.Year == 2000).ToList();
			}
		}

		public List<Person> FindByCondition(Func<Person, bool> condition)
		{
			return People.Where(condition).ToList();
		}

		public Person FindById(int id)
		{
			return People.Where(p => p.Id == id).FirstOrDefault();
		}

		public List<Person> GetAll()
		{
			return People;
		}

		public List<Person> GetMales()
		{
			return People.Where(p => p.Gender == GenderType.Male).ToList();
		}

		public Person GetOldest()
		{
			return People.OrderBy(p => p.DateOfBirth).FirstOrDefault();
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
