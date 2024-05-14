using NashTechAssignmentDay6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay6.Application.Common.Interfaces
{
	public interface IWorkTaskRepository
	{
		IEnumerable<WorkTask> GetWorkTasks();
		IEnumerable<WorkTask> FindByCondition(Func<WorkTask,bool> condition);
		bool Create(WorkTask workTask);
		bool CreateRange(IEnumerable<WorkTask> workTasks);
		bool Update(WorkTask workTask);
		bool Delete(WorkTask workTask);
	}
}
