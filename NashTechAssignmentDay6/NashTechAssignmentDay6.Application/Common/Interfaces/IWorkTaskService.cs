using NashTechAssignmentDay6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay6.Application.Common.Interfaces
{
	public interface IWorkTaskService
	{
		IEnumerable<WorkTask> GetAllWorkTasks();
		WorkTask GetWorkTaskById(int id);
		bool Remove(WorkTask workTask);
		bool Update(WorkTask workTask);
		bool Create(WorkTask workTask);
		bool CreateRange(IEnumerable<WorkTask> workTasks);
		bool RemoveRange(IEnumerable<WorkTask> workTasks);
	}
}
