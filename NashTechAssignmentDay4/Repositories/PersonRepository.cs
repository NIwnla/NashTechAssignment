using NashTechAssignmentDay4.Extensions;
using NashTechAssignmentDay4.Interfaces;
using NashTechAssignmentDay4.Models;
using System.Linq.Expressions;

namespace NashTechAssignmentDay4.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private List<Person> People;
		public PersonRepository()
		{
			People = new List<Person>().GetDataFromFile();
		}
		public List<Person> FindByCondition(Func<Person, bool> condition)
		{
			return People.Where(condition).ToList();
		}

		public List<Person> GetAll()
		{
			return People;
		}
	}
}
