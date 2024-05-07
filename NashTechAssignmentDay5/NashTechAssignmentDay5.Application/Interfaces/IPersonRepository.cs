using NashTechAssignmentDay5.Domain.Entities;

namespace NashTechAssignmentDay5.Application.Interfaces
{
	public interface IPersonRepository
	{
		List<Person> GetAll();
		List<Person> FindByCondition(Func<Person, bool> condition);
		bool Create(Person person);
		bool Update(Person person);
		bool Delete(Person person);
	}
}
