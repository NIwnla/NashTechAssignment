using Data;
using Newtonsoft.Json;

namespace Day1Assignment5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var list = new List<Student>();
			list = list.GetData();
			int index = 0;
			while (true)
			{
				if (list[index].BirthPlace == "Ha Noi")
				{
					Console.WriteLine(JsonConvert.SerializeObject(list[index]));
					break;
				}
				index++;
				if (index == list.Count)
				{
					Console.WriteLine("There no one born in Ha Noi");
					break;
				}
			}
		}
	}
}
