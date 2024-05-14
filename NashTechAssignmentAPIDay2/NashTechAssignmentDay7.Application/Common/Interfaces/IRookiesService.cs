using NashTechAssignmentDay7.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay7.Application.Common.Interfaces
{
	public interface IRookiesService
	{
		IEnumerable<Person> GetAllPeople();
		IEnumerable<Person> GetPeopleByFilter(string? firstName, string? lastName, string? gender, string? birthPlace);
		bool Remove(Person person);
		bool Update(Person person);
		bool Create(Person person);
		bool CreateRange(IEnumerable<Person> people);
		bool RemoveRange(IEnumerable<Person> people);
	}
}
