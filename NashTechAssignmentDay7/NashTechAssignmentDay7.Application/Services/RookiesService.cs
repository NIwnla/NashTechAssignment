using NashTechAssignmentDay7.Application.Common.Interfaces;
using NashTechAssignmentDay7.Domain.Entities;

namespace NashTechAssignmentDay7.Application.Services
{
	public class RookiesService : IRookiesService
	{
		private readonly IPersonRepository _personRepository;
		public RookiesService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}
		public bool Create(Person person)
		{
			return _personRepository.Create(person);
		}

		public bool CreateRange(IEnumerable<Person> people)
		{
			return _personRepository.CreateRange(people);
		}

		public IEnumerable<Person> GetAllPeople()
		{
			return _personRepository.GetWorkTasks();
		}

		public IEnumerable<Person> GetPeopleByFilter(string filter)
		{
			return _personRepository.FindByCondition(p => (p.FirstName + " " + p.LastName).Contains(filter)
														|| string.Equals(p.Gender.ToString(), filter, StringComparison.OrdinalIgnoreCase)
														|| p.BirthPlace.ToLower().Contains(filter.ToLower()));
		}

		public bool Remove(Person person)
		{
			return _personRepository.Delete(person);
		}

		public bool RemoveRange(IEnumerable<Person> people)
		{
			foreach (var person in people)
			{
				if (!_personRepository.Delete(person)) return false;
			}
			return true;
		}

		public bool Update(Person person)
		{
			return _personRepository.Update(person);
		}
	}
}
