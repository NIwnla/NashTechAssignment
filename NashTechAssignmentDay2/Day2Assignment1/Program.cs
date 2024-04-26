using Data;
using Newtonsoft.Json;

namespace Day2Assignment1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var list = new List<Student>().GetData();
			// 1 Find all male students
			var maleList = list.Where(s => s.Gender == true).ToList();
			Console.WriteLine("List of male students: ");
			maleList.WriteConsole<Student>();
			Console.WriteLine();

			// 2 Find oldest student
			var maxAgeList = list.Where(s => s.Age == list.Max(s => s.Age)).ToList();
			Console.WriteLine("Oldest student: ");
			if(maxAgeList.Count > 1)
			{
				var oldestStudent = maxAgeList.OrderBy(s => s.DOB).FirstOrDefault();
				Console.WriteLine(JsonConvert.SerializeObject(oldestStudent));
			}
			else
			{
				Console.WriteLine(JsonConvert.SerializeObject(maxAgeList.FirstOrDefault()));
			}
			Console.WriteLine();

			// 3 Create a list with full name of students
			Console.WriteLine("Full name of all students: ");
			var firstLastNameList = list.Select(s => new {s.FirstName , s.LastName});
			var fullNameList = new List<string>();
			foreach( var firstLastName in firstLastNameList) 
			{
				fullNameList.Add(firstLastName.FirstName + " " + firstLastName.LastName);
			}
			fullNameList.WriteConsole<string>();
			Console.WriteLine();

			// 4 Create 3 list of students with birth year less, equal, more than 2000
			Console.WriteLine("Student with birth year is 2000: ");
			var equal2000 = list.Where(s => s.DOB.Value.Year == 2000).ToList();
			equal2000.WriteConsole<Student>();
			Console.WriteLine();
			Console.WriteLine("Student with birth year less than 2000: ");
			var less2000 = list.Where(s => s.DOB.Value.Year < 2000).ToList();
			less2000.WriteConsole<Student>();
			Console.WriteLine();
			Console.WriteLine("Student with birth year more than 2000: ");
			var more2000 = list.Where(s => s.DOB.Value.Year > 2000).ToList();
			more2000.WriteConsole<Student>();
			Console.WriteLine();


			// 5 Find the first student who born in Ha Noi
			Console.WriteLine("First student who born in Hanoi is:");
			var firstBornHaNoi = list.Where(s => s.BirthPlace == "Ha Noi").FirstOrDefault();
			if(firstBornHaNoi != null)
			{
				Console.WriteLine(JsonConvert.SerializeObject(firstBornHaNoi));
			}
			else
			{
				Console.WriteLine("No one");
			}
		}
	}
}
