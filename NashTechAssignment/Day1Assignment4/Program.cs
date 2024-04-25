using Data;
using Newtonsoft.Json;

namespace Day1Assignment4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var list = new List<Student>();
			list = list.GetData();
			var equal2000 = new List<Student>();
			var more2000 = new List<Student>();
			var less2000 = new List<Student>();
			foreach(Student student in list)
			{
				switch(student.DOB.Value.Year) 
				{
					case 2000:
						equal2000.Add(student); 
						break;
					case > 2000:
						more2000.Add(student);
						break;
					case < 2000:
						less2000.Add(student);
						break;
				}
			}
			Console.WriteLine("Student with birth year equal to 2000: ");
			foreach (Student student in equal2000)
			{
				Console.WriteLine(JsonConvert.SerializeObject(student));
			}
			Console.WriteLine();
			Console.WriteLine("Student with birth year greater than 2000: ");
			foreach (Student student in more2000)
			{
				Console.WriteLine(JsonConvert.SerializeObject(student));
			}
			Console.WriteLine();
			Console.WriteLine("Student with birth year less than 2000: ");
			foreach (Student student in less2000)
			{
				Console.WriteLine(JsonConvert.SerializeObject(student));
			}
			Console.ReadLine();
		}
	}
}
