using NashTechAssignmentDay4.Interfaces;
using NashTechAssignmentDay4.Models;

namespace NashTechAssignmentDay4.Services
{
	public class RookiesService
	{
		private readonly IPersonRepository _personRepository;
		public RookiesService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		public List<Person> GetPeople()
		{
			return _personRepository.GetAll();
		}

		public List<Person> GetMales()
		{
			return _personRepository.FindByCondition(p => p.Gender == GenderType.Male);
		}

		public Person GetOldest()
		{
			return _personRepository.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault();
		}

		public object GetFullnames()
		{
			return _personRepository.GetAll().Select(p => new { FirstName = p.FirstName, LastName = p.LastName }).ToList();
		}

		public List<Person> GetByBirthYear(int input)
		{
			switch (input)
			{
				case 1:
					return _personRepository.FindByCondition(p => p.DateOfBirth.Year > 2000);
				case 2:
					return _personRepository.FindByCondition(p => p.DateOfBirth.Year < 2000);
				default:
					return _personRepository.FindByCondition(p => p.DateOfBirth.Year == 2000);
			}
		}
	}
}
