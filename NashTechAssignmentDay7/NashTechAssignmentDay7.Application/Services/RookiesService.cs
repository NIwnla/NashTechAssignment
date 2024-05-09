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
			return _personRepository.GetPeople();
		}

		public IEnumerable<Person> GetPeopleByFilter(string? firstName, string? lastName, string? gender, string? birthPlace)
		{
			return _personRepository.GetPeople().Where(x => 
										(string.IsNullOrEmpty(firstName) || (!string.IsNullOrEmpty(firstName) && x.FirstName.Contains(firstName)))
									&& (string.IsNullOrEmpty(lastName) || (!string.IsNullOrEmpty(lastName) && x.LastName.Contains(lastName)))
									&& (string.IsNullOrEmpty(gender) || (!string.IsNullOrEmpty(gender) && x.Gender.ToString().ToLower() == gender.ToLower()))
									&& (string.IsNullOrEmpty(birthPlace) || (!string.IsNullOrEmpty(birthPlace) && x.BirthPlace.ToLower().Contains(birthPlace.ToLower()))));

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
