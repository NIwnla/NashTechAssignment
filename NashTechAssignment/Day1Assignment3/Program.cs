using Data;

namespace Day1Assignment3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var list = new List<Student>().GetData();
			foreach (Student student in list)
			{
				Console.WriteLine(student.FirstName + " " + student.LastName);
			}
			Console.ReadLine();
		}
	}
}
