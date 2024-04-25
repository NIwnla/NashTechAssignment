using Data;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Day1Assignment2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var list = new List<Student>();
			list = list.GetData();
			int maxAge = 0;

			// Find oldest age
			foreach (Student student in list)
			{
				if (student.Age > maxAge)
				{
					maxAge = student.Age.Value;
				}
			}

			// Add oldest age student to list
			var maxAgeList = new List<Student>();
			foreach (Student student in list)
			{
				if (student.Age == maxAge)
				{
					maxAgeList.Add(student);
				}
			}

			// If list of oldest age students have more than 1 record
			// Find earliest DOB and show
			// Else just show 1st record of list
			if (maxAgeList.Count >= 1)
			{
				var minDate = DateTime.MaxValue;
				foreach(Student student in maxAgeList)
				{
					if(student.DOB < minDate)
					{
						minDate = student.DOB.Value;
					}
				}
				foreach(Student student in maxAgeList)
				{
					if(student.DOB == minDate)
					{
						Console.WriteLine(JsonConvert.SerializeObject(student));
					}
				}
			}
			else
			{
				Console.WriteLine(JsonConvert.SerializeObject(maxAgeList[0]));
			}
			Console.ReadLine();
		}
	}
}
