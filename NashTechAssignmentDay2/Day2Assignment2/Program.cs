using System.Diagnostics;

namespace Day2Assignment2
{
	internal class Program
	{
		static bool IsPrime(int number)
		{
			// greater than 1
			if (number <= 1)
				return false;
			// 2 and 3 are prime number return true
			if (number <= 3)
				return true;
			// Quickly find non-prime number that divisible by 2, 3
			if (number % 2 == 0 || number % 3 == 0)
				return false;

			// Prime number greater than 3 can be represent in form of 6k +- 1 where k is an integer
			// if the number checking is divisible by any number less than or equal to its square root
			for (int i = 5; i * i <= number; i += 6)
			{
				if (number % i == 0 || number % (i + 2) == 0)
					return false;
			}
			return true;
		}

		static void PrintPrimeInRange(int start, int end)
		{
			for (int num = start; num <= end; num++)
			{
				if (IsPrime(num))
					Console.Write(num + " ");
			}
		}

		//Split range of numbers into multiple task that run parallel
		static async Task CreateRangeFindPrimeNumbers(int start, int end, int rangeSize = 0)
		{
			// If rangeSize = 0, assume no splitting needed
			if (rangeSize == 0)
			{
				PrintPrimeInRange(start, end);
				return;
			}
			var taskList = new List<Task>();
			// Split range into multiple task to run parallel in finding prime numbers
			for (int i = start; i <= end; i += rangeSize)
			{
				int rangeStart = i;
				int rangeEnd = Math.Min(i + rangeSize - 1, end);
				taskList.Add(Task.Run(() => PrintPrimeInRange(rangeStart, rangeEnd)));
			}
			await Task.WhenAll(taskList);
		}

		//Check for int
		static int? checkInt(string? value)
		{
			try
			{
				return int.Parse(value);
			}
			catch (Exception)
			{
				Console.WriteLine("Please enter an integer");
				return null;
			}
		}

		static async Task Main(string[] args)
		{
			int? start = default;
			int? end = default;
			int? range = default;
			Console.WriteLine("Enter the range of integers to find prime numbers:");
			while (start == null)
			{
				Console.Write("From: ");
				start = checkInt(Console.ReadLine());
			}
			while (end == null)
			{
				Console.Write("To: ");
				end = checkInt(Console.ReadLine());
			}
			while (range == null)
			{
				Console.Write("Range to split for tasks (0 if you dont want to split):");
				range = checkInt(Console.ReadLine());
			}

			// Add a stopwatch to check time to complete
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine($"Prime numbers within the range {start} - {end}:");

			// Start finding prime number
			await CreateRangeFindPrimeNumbers(start.Value, end.Value, range.Value);

			stopwatch.Stop();
			Console.WriteLine();
			Console.WriteLine($"Program executed in {stopwatch.Elapsed.TotalMilliseconds} miliseconds");
			Console.WriteLine($"Finding prime number in {start} and {end}");
			if (range == 0) range = start + end;
			Console.WriteLine($"With {(start + end) / range} Tasks");
		}
	}
}
