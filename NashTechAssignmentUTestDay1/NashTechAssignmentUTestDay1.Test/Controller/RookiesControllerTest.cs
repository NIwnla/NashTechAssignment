using FluentAssertions;
using Moq;
using NashTechAssignmentDay5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay5.Areas.NashTech.Controllers;
using NashTechAssignmentDay5.Domain.Entities;
using DocumentFormat.OpenXml.Office2013.Word;
using Person = NashTechAssignmentDay5.Domain.Entities.Person;
using NashTechAssignmentDay5.Application.Helper;
using Microsoft.AspNetCore.Routing;

namespace NashTechAssignmentUTestDay1.Test.Controller
{
	[TestFixture]
	public class RookiesControllerTest : IDisposable
	{
		private Mock<IRookiesService> _mockService;
		private RookiesController _controller;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mockService = new Mock<IRookiesService>(MockBehavior.Strict);
			_controller = new RookiesController(_mockService.Object);
		}

		public void Dispose()
		{
			_controller = null;
			_mockService = null;
		}

		[Test]
		public void Index_ReturnView()
		{
			//Arrange
			int? pageNumber = 1;
			int? pageSize = 3;
			var peopleList = new Mock<List<Person>>();
			_mockService.Setup(ser => ser.GetPeople()).Returns(peopleList.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(peopleList.Object.AsQueryable(), pageNumber ?? 1, pageSize.Value);

			//Act
			var result = _controller.Index(pageNumber, pageSize) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void Details_WithId_ReturnNotFound()
		{
			//Arrange
			Person person = null;
			int id = 1;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person);

			//Act
			var result = _controller.Details(id) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void Details_WithId_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);

			//Act
			var result = _controller.Details(id) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Create_ReturnView()
		{
			//Arrange

			//Act
			var result = _controller.Create() as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<ViewResult>();
		}

		[Test]
		public void Create_WithValidPerson_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.AddPerson(person.Object)).Returns(false);

			//Act
			var result = _controller.Create(person.Object) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Create_WithValidPerson_ReturnRedirectToAction()
		{
			////Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.AddPerson(person.Object)).Returns(true);
			var expectedRedirectValues = new RouteValueDictionary
			{
				{ "action", "Index" }
			};
			//Act
			var result = _controller.Create(person.Object) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void Delete_WithId_ReturnProblem()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);
			_mockService.Setup(ser => ser.DeletePerson(id)).Returns(false);

			//Act
			var result = _controller.Delete(id) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void Delete_WithId_ReturnRedirectToAction()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);
			_mockService.Setup(ser => ser.DeletePerson(id)).Returns(true);

			//Act
			var result = _controller.Delete(id) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void ConfirmDelete_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();

			//Act
			var result = _controller.ConfirmDelete(person.Object) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Edit_WithId_ReturnNotFound()
		{
			//Arrange
			Person person = null;
			int id = 1;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person);

			//Act
			var result = _controller.Edit(id) as ActionResult;

			//Assert
			result.Should().NotBeNull();

		}

		[Test]
		public void Edit_WithId_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);

			//Act
			var result = _controller.Edit(id) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Edit_WithValidPerson_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.EditPerson(person.Object)).Returns(false);

			//Act
			var result = _controller.Edit(person.Object) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Edit_WithValidPerson_ReturnRedirectToAction()
		{
			//Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.EditPerson(person.Object)).Returns(true);

			//Act
			var result = _controller.Edit(person.Object) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void GetMales_ReturnView()
		{
			//Arrange
			var males = new Mock<List<Person>>();
			_mockService.Setup(ser => ser.GetMales()).Returns(males.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(males.Object.AsQueryable(), 1, males.Object.Count);

			//Act
			var result = _controller.GetMales() as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void GetOldest_ReturnView()
		{
			//Arrange
			var oldest = new Mock<Person>();
			_mockService.Setup(ser => ser.GetOldest()).Returns(oldest.Object);

			//Act
			var result = _controller.GetOldest() as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(oldest.Object);
		}

		[Test]
		public void GetFullNames_ReturnView()
		{
			//Arrange
			var fullnames = new Mock<List<Person>>();
			_mockService.Setup(ser => ser.GetMales()).Returns(fullnames.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(fullnames.Object.AsQueryable(), 1, fullnames.Object.Count);

			//Act
			var result = _controller.GetMales() as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void GetByBirthYear_WithNull_ReturnBadRequest()
		{
			//Arrange
			int? input = null;

			//Act
			var result = _controller.GetByBirthYear(input) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void GetByBirthYear_WithNull_ReturnView()
		{
			//Arrange
			int input = 1;
			var people = new Mock<List<Person>>();
			_mockService.Setup(ser => ser.GetByBirthYear(input)).Returns(people.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(people.Object.AsQueryable(), 1, people.Object.Count);
			//Act
			var result = _controller.GetByBirthYear(input) as ViewResult;

			//Assert
			result.Should().NotBeNull();
			result.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void ExportToExcel_ReturnView()
		{
			//Arrange
			string? path = null;
			_mockService.Setup(ser => ser.ExportToExcel(path)).Returns(true);

			//Act
			var result = _controller.ExportToExcel(path) as ViewResult;

			//Assert
			result.Should().NotBeNull();
		}

		[Test]
		public void ExportToExcel_ReturnProblem()
		{
			//Arrange
			string? path = null;
			_mockService.Setup(ser => ser.ExportToExcel(path)).Returns(false);

			//Act
			var result = _controller.ExportToExcel(path) as ActionResult;

			//Assert
			result.Should().NotBeNull();
		}

	}
}
