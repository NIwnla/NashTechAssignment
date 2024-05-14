using NashTechAssignmentDay6.Application.Common.Interfaces;
using NashTechAssignmentDay6.Domain.Entities;
using NashTechAssignmentDay6.Infrastructure.Data;

namespace NashTechAssignmentDay6.Application.Repositories
{
	public class WorkTaskRepository : IWorkTaskRepository
	{
		private List<WorkTask> WorkTasks;
		public WorkTaskRepository() 
		{
			WorkTasks = new List<WorkTask>().GetDataFromFile();
		}
		public bool Create(WorkTask workTask)
		{
			WorkTasks.Add(workTask);
			return WorkTasks.SaveDataToFile();
		}

		public bool CreateRange(IEnumerable<WorkTask> workTasks)
		{
			WorkTasks.AddRange(workTasks);
			return WorkTasks.SaveDataToFile();
		}

		public bool Delete(WorkTask workTask)
		{
			var workTaskToDelete = WorkTasks.Find(w => w.Id == workTask.Id);
			WorkTasks.Remove(workTaskToDelete);
			return WorkTasks.SaveDataToFile();
		}

		public IEnumerable<WorkTask> FindByCondition(Func<WorkTask, bool> condition)
		{
			return WorkTasks.Where(condition);
		}

		public IEnumerable<WorkTask> GetWorkTasks()
		{
			return WorkTasks;
		}

		public bool Update(WorkTask workTask)
		{
			var workTaskToUpdate = WorkTasks.Find(w => w.Id == workTask.Id);
			workTaskToUpdate.Title = workTask.Title;
			workTaskToUpdate.IsCompleted = workTask.IsCompleted;
			return WorkTasks.SaveDataToFile();
		}
	}
}
