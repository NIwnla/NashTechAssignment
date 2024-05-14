using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NashTechAssignmentDay4.Extensions;
using NashTechAssignmentDay4.Interfaces;
using NashTechAssignmentDay4.Models;
using NashTechAssignmentDay4.Services;

namespace NashTechAssignmentDay4.Controllers
{
    [Route("NashTech/Rookies")]
    public class RookiesController : Controller
    {
        private readonly RookiesService _service;
        public RookiesController(RookiesService service)
        {
			_service = service;
        }

        [Route("GetMales")]
        public IActionResult GetMales()
        {
            var males = _service.GetMales();
            if (males.Count <= 0)
            {
                return NotFound("No male found");
            }
            return Ok(males);
        }

        [Route("GetOldest")]
        public IActionResult GetOldest()
        {
            var oldestPerson = _service.GetOldest();
            if (oldestPerson == null)
            {
                return NotFound("There is no people in the list");
            }
            return Ok(oldestPerson);
        }

        [Route("GetFullNames")]
        public IActionResult GetFullNames()
        {
            var fullnames = _service.GetFullnames();
            return Ok(fullnames);
        }

        [Route("GetByBirthYear")]
        public IActionResult GetByBirthYear([FromQuery] int? input)
        {
            if(input == null)
            {
                return BadRequest("Please input your choice");
            }
            var result = _service.GetByBirthYear(input.Value);
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
                var people = _service.GetPeople();

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

                workbook.SaveAs("People.xlsx");
                return Ok("Successfully export to excel");
            }
            catch
            {
                return Problem("Problem with saving data to excel file");
            }
        }
    }
}
