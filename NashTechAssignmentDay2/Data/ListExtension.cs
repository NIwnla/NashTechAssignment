using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public static class ListExtension
	{
		public static List<Student> GetData(this List<Student> list)
		{
			var fullPath = "D:\\VSProject\\Repos\\NashTechAssignment\\NashTechAssignmentDay2\\Data\\MOCK_DATA.json";
			if (File.Exists(fullPath))
			{
				using (StreamReader r = new StreamReader(fullPath))
				{
					string json = r.ReadToEnd();
					list = JsonConvert.DeserializeObject<List<Student>>(json);
				}
			}
			return list;
		}
		public static void WriteConsole<T>(this List<T> list)
		{
			foreach (var item in list)
			{
				Console.WriteLine(JsonConvert.SerializeObject(item));
			}
		}
	}
}
