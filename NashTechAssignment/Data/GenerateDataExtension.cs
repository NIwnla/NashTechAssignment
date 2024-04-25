using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public static class GenerateDataExtension
	{
		public static List<Student> GetData(this List<Student> list)
		{
			var fullPath = "D:\\VSProject\\Repos\\NashTechAssignment\\NashTechAssignment\\Data\\MOCK_DATA.json";
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
	}
}
