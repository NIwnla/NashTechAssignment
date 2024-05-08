using NashTechAssignmentDay6.Domain.Entities;
using Newtonsoft.Json;

namespace NashTechAssignmentDay6.Infrastructure.Data
{
	public static class ListExtension
	{
		private static readonly string DATA_FILE_PATH = @"\NashTechAssignmentDay6.Infrastructure\Database.json";

		public static List<WorkTask> GetDataFromFile(this List<WorkTask> workTasks)
		{
			string fileDirectory = GetDataFilePath();
			if (File.Exists(fileDirectory))
			{
				using (StreamReader reader = new StreamReader(fileDirectory))
				{
					string json = reader.ReadToEnd();
					workTasks = JsonConvert.DeserializeObject<List<WorkTask>>(json);
				}
			}
			return workTasks;
		}

		public static bool SaveDataToFile(this List<WorkTask> workTasks)
		{
			string fileDirectory = GetDataFilePath();
			if (File.Exists(fileDirectory))
			{
				using (StreamWriter writer = new StreamWriter(fileDirectory))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(writer, workTasks);
					return true;
				}
			}
			return false;
		}

		private static string GetDataFilePath()
		{
			var relativePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
			return relativePath + DATA_FILE_PATH;
		}
	}

}
