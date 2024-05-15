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
			var expectedMessage = $"No person with id: {id} found";
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person);

			//Act
			var result = _controller.Details(id);

			//Assert
			result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be(expectedMessage);
		}

		[Test]
		public void Details_WithId_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);

			//Act
			var result = _controller.Details(id);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Create_ReturnView()
		{
			//Arrange

			//Act
			var result = _controller.Create();

			//Assert
			result.Should().BeOfType<ViewResult>();
		}

		[Test]
		public void Create_WithValidPerson_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.AddPerson(person.Object)).Returns(false);

			//Act
			var result = _controller.Create(person.Object);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(person.Object);
		}

		[Test]
		public void Create_WithValidPerson_ReturnRedirectToAction()
		{
			////Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.AddPerson(person.Object)).Returns(true);
			var expectedAction = "Index";
			//Act
			var result = _controller.Create(person.Object);

			//Assert
			result.Should().BeOfType<RedirectToActionResult>();
			var redirectToActionResult = result as RedirectToActionResult;
			redirectToActionResult.Should().NotBeNull();
			redirectToActionResult!.ActionName.Should().Be(expectedAction);
		}

		[Test]
		public void Delete_WithId_ReturnProblem()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);
			_mockService.Setup(ser => ser.DeletePerson(id)).Returns(false);
			var expectedMessage = "Problem with deleting person";

			//Act
			var result = _controller.Delete(id);

			//Assert
			result.Should().BeOfType<ObjectResult>()
				.Which.Value.Should().BeOfType<ProblemDetails>()
				.Which.Detail.Should().Be(expectedMessage);
		}

		[Test]
		public void Delete_WithId_ReturnRedirectToAction()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			var expectedAction = "ConfirmDelete";
			var expectedRouteValue = person.Object;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);
			_mockService.Setup(ser => ser.DeletePerson(id)).Returns(true);

			//Act
			var result = _controller.Delete(id);

			//Assert
			result.Should().BeOfType<RedirectToActionResult>();
			var redirectToActionResult = result as RedirectToActionResult;
			redirectToActionResult.Should().NotBeNull();
			redirectToActionResult!.ActionName.Should().Be(expectedAction);
			redirectToActionResult.RouteValues.Should().ContainKey("Id").WhoseValue.Should().Be(expectedRouteValue.Id);
			redirectToActionResult.RouteValues.Should().ContainKey("FirstName").WhoseValue.Should().Be(expectedRouteValue.FirstName);
			redirectToActionResult.RouteValues.Should().ContainKey("LastName").WhoseValue.Should().Be(expectedRouteValue.LastName);
			redirectToActionResult.RouteValues.Should().ContainKey("PhoneNumber").WhoseValue.Should().Be(expectedRouteValue.PhoneNumber);
			redirectToActionResult.RouteValues.Should().ContainKey("DateOfBirth").WhoseValue.Should().Be(expectedRouteValue.DateOfBirth);
			redirectToActionResult.RouteValues.Should().ContainKey("Gender").WhoseValue.Should().Be(expectedRouteValue.Gender);
			redirectToActionResult.RouteValues.Should().ContainKey("BirthPlace").WhoseValue.Should().Be(expectedRouteValue.BirthPlace);
			redirectToActionResult.RouteValues.Should().ContainKey("IsGraduated").WhoseValue.Should().Be(expectedRouteValue.IsGraduated);
		}

		[Test]
		public void ConfirmDelete_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();

			//Act
			var result = _controller.ConfirmDelete(person.Object);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(person.Object);
		}

		[Test]
		public void Edit_WithId_ReturnNotFound()
		{
			//Arrange
			Person person = null;
			int id = 1;
			var expectedMessage = $"No person with id: {id} found";
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person);

			//Act
			var result = _controller.Edit(id);

			//Assert
			result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be(expectedMessage);

		}

		[Test]
		public void Edit_WithId_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			int id = person.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(person.Object);

			//Act
			var result = _controller.Edit(id);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(person.Object);
		}

		[Test]
		public void Edit_WithValidPerson_ReturnView()
		{
			//Arrange
			var person = new Mock<Person>();
			_mockService.Setup(ser => ser.EditPerson(person.Object)).Returns(false);

			//Act
			var result = _controller.Edit(person.Object);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(person.Object);
		}

		[Test]
		public void Edit_WithValidPerson_ReturnRedirectToAction()
		{
			//Arrange
			var person = new Mock<Person>();
			var expectedAction = "Index";
			_mockService.Setup(ser => ser.EditPerson(person.Object)).Returns(true);

			//Act
			var result = _controller.Edit(person.Object);

			//Assert
			result.Should().BeOfType<RedirectToActionResult>();
			var redirectToActionResult = result as RedirectToActionResult;
			redirectToActionResult.Should().NotBeNull();
			redirectToActionResult!.ActionName.Should().Be(expectedAction);
		}

		[Test]
		public void GetMales_ReturnView()
		{
			//Arrange
			var males = new Mock<List<Person>>();
			_mockService.Setup(ser => ser.GetMales()).Returns(males.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(males.Object.AsQueryable(), 1, males.Object.Count);

			//Act
			var result = _controller.GetMales();

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void GetOldest_ReturnView()
		{
			//Arrange
			var oldest = new Mock<Person>();
			_mockService.Setup(ser => ser.GetOldest()).Returns(oldest.Object);

			//Act
			var result = _controller.GetOldest();

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(oldest.Object);
		}

		[Test]
		public void GetFullNames_ReturnView()
		{
			//Arrange
			var fullnames = new Mock<List<Person>>();
			_mockService.Setup(ser => ser.GetMales()).Returns(fullnames.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(fullnames.Object.AsQueryable(), 1, fullnames.Object.Count);

			//Act
			var result = _controller.GetMales();

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void GetByBirthYear_WithNull_ReturnBadRequest()
		{
			//Arrange
			int? input = null;
			var expectedMessage = "Please input your choice";

			//Act
			var result = _controller.GetByBirthYear(input);

			//Assert
			result.Should().BeOfType<BadRequestObjectResult>();
			var badRequestResult = result as BadRequestObjectResult;
			badRequestResult.Should().NotBeNull();
			badRequestResult!.Value.Should().Be(expectedMessage);
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
			var result = _controller.GetByBirthYear(input);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(paginatedPeopleList);
		}

		[Test]
		public void ExportToExcel_ReturnView()
		{
			//Arrange
			string? path = null;
			_mockService.Setup(ser => ser.ExportToExcel(path)).Returns(true);

			//Act
			var result = _controller.ExportToExcel(path);

			//Assert
			result.Should().BeOfType<ViewResult>();
		}

		[Test]
		public void ExportToExcel_ReturnProblem()
		{
			//Arrange
			string? path = null;
			var expectedMessage = "Can't export to excel";
			_mockService.Setup(ser => ser.ExportToExcel(path)).Returns(false);

			//Act
			var result = _controller.ExportToExcel(path);

			//Assert
			result.Should().BeOfType<ObjectResult>()
				.Which.Value.Should().BeOfType<ProblemDetails>()
				.Which.Detail.Should().Be(expectedMessage);
		}

	}
}
