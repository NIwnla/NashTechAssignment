using NashTechAssignmentDay6.Application.Common.Interfaces;
using NashTechAssignmentDay6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay6.Application.Services
{
	public class WorkTaskService : IWorkTaskService
	{
		private readonly IWorkTaskRepository _workTaskRepository;
		public WorkTaskService(IWorkTaskRepository workTaskRepository)
		{
			_workTaskRepository = workTaskRepository;
		}
		public bool Create(WorkTask workTask)
		{
			return _workTaskRepository.Create(workTask);
		}

		public bool CreateRange(IEnumerable<WorkTask> workTasks)
		{
			return _workTaskRepository.CreateRange(workTasks);
		}

		public IEnumerable<WorkTask> GetAllWorkTasks()
		{
			return _workTaskRepository.GetWorkTasks();
		}

		public WorkTask GetWorkTaskById(int id)
		{
			return _workTaskRepository.FindByCondition(w => w.Id == id).FirstOrDefault();
		}

		public bool Remove(WorkTask workTask)
		{
			return _workTaskRepository.Delete(workTask);
		}

		public bool RemoveRange(IEnumerable<WorkTask> workTasks)
		{
			foreach (var workTask in workTasks)
			{
				if (!_workTaskRepository.Delete(workTask)) return false;
			}
			return true;
		}

		public bool Update(WorkTask workTask)
		{
			return _workTaskRepository.Update(workTask);
		}
	}
}
