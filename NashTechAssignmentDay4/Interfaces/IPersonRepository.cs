using NashTechAssignmentDay4.Models;
using System.Linq.Expressions;

namespace NashTechAssignmentDay4.Interfaces
{
	public interface IPersonRepository
	{
		List<Person> GetAll();
		List<Person> FindByCondition(Func<Person, bool> condition);
	}
}
