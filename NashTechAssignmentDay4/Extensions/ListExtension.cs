using NashTechAssignmentDay4.Models;
using Newtonsoft.Json;

namespace NashTechAssignmentDay4.Extensions
{
    public static class ListExtension
    {
        public static List<Person> GetDataFromFile(this List<Person> list)
        {
            string projectDirectory = Environment.CurrentDirectory;
            string fileDirectory = projectDirectory + @"\Data\MockData.json";
            if (File.Exists(fileDirectory))
            {
                using (StreamReader r = new StreamReader(fileDirectory))
                {
                    string json = r.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<Person>>(json);
                }
            }
            return list;
        }
    }

}
