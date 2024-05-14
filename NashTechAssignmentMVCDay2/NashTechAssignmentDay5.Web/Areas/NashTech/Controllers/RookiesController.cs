using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay5.Application.Helper;
using NashTechAssignmentDay5.Domain.Entities;
using NashTechAssignmentDay5.Application.Interfaces;

namespace NashTechAssignmentDay5.Areas.NashTech.Controllers
{
    [Area("NashTech")]
    public class RookiesController : Controller
    {
        private readonly IRookiesService _service;
        public RookiesController(IRookiesService service)
        {
            _service = service;
        }

        public IActionResult Index(int? pageNumber, int? pageSize = 3)
        {
            var peopleQuery = _service.GetPeople().AsQueryable();
            return View(PaginatedList<Person>.Create(peopleQuery, pageNumber ?? 1, pageSize.Value));
        }

        public IActionResult Details(int id)
        {
            var person = _service.GetPersonById(id);
            if (person == null)
            {
                return NotFound($"No person with id: {id} found");
            }
            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            if (_service.AddPerson(person))
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public IActionResult Delete(int id)
        {
            var deletedPerson = _service.GetPersonById(id);
            if (_service.DeletePerson(id))
            {
                return RedirectToAction("ConfirmDelete", deletedPerson);
            }
            return Problem("Problem with deleting person");
        }

        public IActionResult ConfirmDelete(Person person)
        {
            return View(person);
        }

        public IActionResult Edit(int id)
        {
            var personToUpdate = _service.GetPersonById(id);
            if (personToUpdate == null)
            {
                return NotFound($"No person with id: {id} found");
            }
            return View(personToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            if (_service.EditPerson(person))
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //[Route("GetMales")]
        public IActionResult GetMales()
        {
            var males = _service.GetMales().AsQueryable();
            return View("Index", PaginatedList<Person>.Create(males, 1, males.Count()));
        }

        //[Route("GetOldest")]
        public IActionResult GetOldest()
        {
            var oldestPerson = _service.GetOldest();
            return View("Details", oldestPerson);
        }

        //[Route("GetFullNames")]
        public IActionResult GetFullNames()
        {
            var people = _service.GetPeople();
            return View(people);
        }

        //[Route("GetByBirthYear")]
        public IActionResult GetByBirthYear([FromQuery] int? input)
        {
            if (input == null)
            {
                return BadRequest("Please input your choice");
            }
            var result = _service.GetByBirthYear(input.Value).AsQueryable();
            return View("Index", PaginatedList<Person>.Create(result, 1, result.Count()));
        }

        //[Route("ExportToExcel")]
        public IActionResult ExportToExcel(string? path)
        {
            if (_service.ExportToExcel(path))
            {
                return View();
            }
            return Problem("Can't export to excel");
        }
    }
}
