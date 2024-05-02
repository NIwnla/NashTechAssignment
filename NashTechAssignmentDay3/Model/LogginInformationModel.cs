using System.IO;

namespace NashTechAssignmentDay3.Model
{
	public class LogginInformationModel
	{
		public HostString Host { get; set; }
		public string Schema { get; set; }
		public string Path { get; set; }
		public QueryString QueryString { get; set; }
		public string RequestBody { get; set; }

		public void WriteObjectToFile()
		{
			//Get current project 's directory
			string projectDirectory = Environment.CurrentDirectory;
			string pathDirectory = projectDirectory + @"\Logs\Request.txt";

			//If there is no Request.txt, create new file
			if (!File.Exists(pathDirectory))
			{
				using (StreamWriter writer = File.CreateText(pathDirectory))
				{
					writer.WriteLine(DateTime.Now);
					writer.WriteLine($"Host: {Host}");
					writer.WriteLine($"Schema: {Schema}");
					writer.WriteLine($"Path: {Path}");
					writer.WriteLine($"QueryString: {QueryString}");
					writer.WriteLine($"RequestBody: {RequestBody}");
				}
			}
			else
			{
				using (StreamWriter writer = File.AppendText(pathDirectory))
				{
					writer.WriteLine();
					writer.WriteLine(DateTime.Now);
					writer.WriteLine($"Host: {Host}");
					writer.WriteLine($"Schema: {Schema}");
					writer.WriteLine($"Path: {Path}");
					writer.WriteLine($"QueryString: {QueryString}");
					writer.WriteLine($"RequestBody: {RequestBody}");
				}
			}

		}
	}
}
