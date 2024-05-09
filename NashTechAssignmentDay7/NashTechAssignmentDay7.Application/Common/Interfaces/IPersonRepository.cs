using NashTechAssignmentDay7.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay7.Application.Common.Interfaces
{
	public interface IPersonRepository
	{
		IEnumerable<Person> GetWorkTasks();
		IEnumerable<Person> FindByCondition(Func<Person,bool> condition);
		bool Create(Person person);
		bool CreateRange(IEnumerable<Person> people);
		bool Update(Person person);
		bool Delete(Person person);
	}
}
