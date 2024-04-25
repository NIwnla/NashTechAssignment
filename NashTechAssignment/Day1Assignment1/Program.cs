using Data;
using Newtonsoft.Json;

namespace Day1Assignment1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var list = new List<Student>().GetData();
			var maleList = new List<Student>();
			foreach(Student student in list)
			{
				if (student.Gender == true) maleList.Add(student);
			}
			maleList.WriteConsole<Student>();
			Console.ReadLine();
		}
	}
}
