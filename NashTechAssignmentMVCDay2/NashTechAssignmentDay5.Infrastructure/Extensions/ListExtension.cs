using NashTechAssignmentDay5.Domain.Entities;
using Newtonsoft.Json;

namespace NashTechAssignmentDay5.Infrastructure.Extensions
{
    public static class ListExtension
    {
        private static readonly string DATA_FILE_PATH = @"\NashTechAssignmentDay5.Infrastructure\Data\MockData.json";

		public static List<Person> GetDataFromFile(this List<Person> people)
        {
            string fileDirectory = GetDataFilePath();
            if (File.Exists(fileDirectory))
            {
                using (StreamReader reader = new StreamReader(fileDirectory))
                {
                    string json = reader.ReadToEnd();
                    people = JsonConvert.DeserializeObject<List<Person>>(json);
                }
            }
            return people;
        }

        public static bool SaveDataToFile(this List<Person> people)
        {
			string fileDirectory = GetDataFilePath();
            if (File.Exists(fileDirectory))
            {
				using (StreamWriter writer = new StreamWriter(fileDirectory))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(writer, people);
                    return true;
				}
			}
            return false;
		}

        private static string GetDataFilePath()
        {
            var relativePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
			return relativePath + DATA_FILE_PATH;
        }
    }

}
