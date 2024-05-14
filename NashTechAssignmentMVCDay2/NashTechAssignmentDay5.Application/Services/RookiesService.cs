using ClosedXML.Excel;
using NashTechAssignmentDay5.Application.Interfaces;
using NashTechAssignmentDay5.Domain.Entities;
using NashTechAssignmentDay5.Domain.Enum;

namespace NashTechAssignmentDay5.Application.Services
{
	public class RookiesService : IRookiesService
	{
		private readonly IPersonRepository _personRepository;
		public RookiesService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		public List<Person> GetPeople()
		{
			return _personRepository.GetAll();
		}

		public Person GetPersonById(int id)
		{
			return _personRepository.FindByCondition(p => p.Id == id).FirstOrDefault();
		}

		public bool EditPerson(Person updatedPerson)
		{
			return _personRepository.Update(updatedPerson);
		}

		public bool DeletePerson(int id)
		{
			var deletePerson = GetPersonById(id);
			return _personRepository.Delete(deletePerson);
		}

		public bool AddPerson(Person addedPerson)
		{
			return _personRepository.Create(addedPerson);
		}

		public List<Person> GetMales()
		{
			return _personRepository.FindByCondition(p => p.Gender == GenderType.Male);
		}

		public Person GetOldest()
		{
			return _personRepository.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault();
		}


		public List<Person> GetByBirthYear(int input)
		{
			switch (input)
			{
				case 1:
					return _personRepository.FindByCondition(p => p.DateOfBirth.Year > 2000);
				case 2:
					return _personRepository.FindByCondition(p => p.DateOfBirth.Year < 2000);
				default:
					return _personRepository.FindByCondition(p => p.DateOfBirth.Year == 2000);
			}
		}

		public bool ExportToExcel(string? path)
		{
			try
			{
				const string FILE_NAME = "People.xlsx";
				var people = _personRepository.GetAll();

				var workbook = new XLWorkbook();
				var worksheet = workbook.AddWorksheet("People");

				//Let cell in excel file ajust its size to match the contents
				worksheet.Columns().AdjustToContents();
				worksheet.Rows().AdjustToContents();

				//Add header
				worksheet.Cell("A1").Value = "First Name";
				worksheet.Cell("B1").Value = "Last Name";
				worksheet.Cell("C1").Value = "Gender";
				worksheet.Cell("D1").Value = "Date of Birth";
				worksheet.Cell("E1").Value = "Phone Number";
				worksheet.Cell("F1").Value = "Birth Place";
				worksheet.Cell("G1").Value = "Is Graduated";

				//Add records
				int row = 2;
				foreach (var person in people)
				{
					worksheet.Cell("A" + row.ToString()).Value = person.FirstName;
					worksheet.Cell("B" + row.ToString()).Value = person.LastName;
					worksheet.Cell("C" + row.ToString()).Value = person.Gender.ToString();
					worksheet.Cell("D" + row.ToString()).Value = person.DateOfBirth;
					worksheet.Cell("E" + row.ToString()).Value = person.PhoneNumber;
					worksheet.Cell("F" + row.ToString()).Value = person.BirthPlace;
					worksheet.Cell("G" + row.ToString()).Value = person.IsGraduated;
					row++;
				}

				if (path != null) path += @"\\";
				workbook.SaveAs(path + FILE_NAME);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
