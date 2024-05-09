﻿using NashTechAssignmentDay7.Application.Common.Interfaces;
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
			var result = _personRepository.GetPeople();
			if (firstName != null)
			{
				result = result.Where(x => x.FirstName.Contains(firstName));
			}
			if (lastName != null)
			{
				result = result.Where(x => x.LastName.Contains(lastName));
			}
			if (gender != null)
			{
				result = result.Where(x => x.Gender.ToString().ToLower() == gender.ToLower());
			}
			if (birthPlace != null)
			{
				result = result.Where(x => x.BirthPlace.ToLower().Contains(birthPlace.ToLower()));
			}
			return result;
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
