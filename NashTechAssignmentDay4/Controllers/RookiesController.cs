using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NashTechAssignmentDay4.Extensions;
using NashTechAssignmentDay4.Interfaces;
using NashTechAssignmentDay4.Models;

namespace NashTechAssignmentDay4.Controllers
{
    [Route("NashTech")]
    public class RookiesController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public RookiesController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Route("GetMales")]
        public IActionResult GetMales()
        {
            var males = _personRepository.FindByCondition(p => p.Gender == GenderType.Male);
            if (males.Count <= 0)
            {
                return NotFound("No male found");
            }
            return Ok(males);
        }

        [Route("GetOldest")]
        public IActionResult GetOldest()
        {
            var oldestPerson = _personRepository.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault();
            if (oldestPerson == null)
            {
                return NotFound("There is no people in the list");
            }
            return Ok(oldestPerson);
        }

        [Route("GetFullNames")]
        public IActionResult GetFullNames()
        {
            var fullnames = _personRepository.GetAll().Select(p => new { FirstName = p.FirstName, LastName = p.LastName }).ToList();
            if (fullnames.Count <= 0)
            {
                return NotFound("No male found");
            }
            return Ok(fullnames);
        }

        [Route("GetByBirthYear")]
        public IActionResult GetByBirthYear([FromQuery] int input = 0)
        {
            var result = new List<Person>();
            switch (input)
            {
                case 0:
                    result = _personRepository.FindByCondition(p => p.DateOfBirth.Year == 2000);
                    break;
                case 1:
                    result = _personRepository.FindByCondition(p => p.DateOfBirth.Year > 2000);
                    break;
                case 2:
                    result = _personRepository.FindByCondition(p => p.DateOfBirth.Year < 2000);
                    break;
            }
            if (result.Count < 0)
            {
                return NotFound("There is no record match your query");
            }
            return Ok(result);
        }

        [Route("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var people = _personRepository.GetAll();

                var workbook = new XLWorkbook();
                workbook.AddWorksheet("People");
                var worksheet = workbook.Worksheet("People");

				worksheet.Cell("A1").Value = "First Name";
                worksheet.Cell("B1").Value = "Last Name";
                worksheet.Cell("C1").Value = "Gender";
                worksheet.Cell("D1").Value = "Date of Birth";
                worksheet.Cell("E1").Value = "Phone Number";
                worksheet.Cell("F1").Value = "Birth Place";
                worksheet.Cell("G1").Value = "Is Graduated";
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

                workbook.SaveAs("People.xlsx");
                return Ok("Successfully export to excel");
            }
            catch (Exception ex)
            {
                return Problem("Problem with saving data to excel file");
            }
        }
    }
}
