using FluentAssertions;
using Moq;
using NashTechAssignmentDay5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay5.Areas.NashTech.Controllers;
using Person = NashTechAssignmentDay5.Domain.Entities.Person;
using NashTechAssignmentDay5.Application.Helper;

namespace NashTechAssignmentUTestDay1.Test.Controller
{
	[TestFixture]
	public class RookiesControllerTest : IDisposable
	{
		private Mock<IRookiesService> _mockService;
		private RookiesController _controller;
		private Mock<Person> _personMock;
		private Mock<List<Person>> _peopleListMock;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mockService = new Mock<IRookiesService>(MockBehavior.Strict);
			_controller = new RookiesController(_mockService.Object);
		}

		[SetUp]
		public void SetUp()
		{
			_personMock = new Mock<Person>();
			_peopleListMock = new Mock<List<Person>>();
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
			//Mock paginatedList view for Index() to return
			int? pageNumber = 1;
			int? pageSize = 3;
			_mockService.Setup(ser => ser.GetPeople()).Returns(_peopleListMock.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(_peopleListMock.Object.AsQueryable(), pageNumber ?? 1, pageSize.Value);

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
			//Method Details(int id) will return NotFound result if service fail to find a record with correspond Id
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
			//This method will return View with record of correspond Id if service can find that record
			int id = _personMock.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(_personMock.Object);

			//Act
			var result = _controller.Details(id);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(_personMock.Object);
		}

		[Test]
		public void Create_ReturnView()
		{
			//Arrange
			//There is no input for this method

			//Act
			var result = _controller.Create();

			//Assert
			result.Should().BeOfType<ViewResult>();
		}

		[Test]
		public void Create_WithValidPerson_ReturnView()
		{
			//Arrange
			//When service failed to create, put back to view with record is currently being created
			_mockService.Setup(ser => ser.AddPerson(_personMock.Object)).Returns(false);

			//Act
			var result = _controller.Create(_personMock.Object);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeEquivalentTo(_personMock.Object);
		}

		[Test]
		public void Create_WithValidPerson_ReturnRedirectToAction()
		{
			////Arrange
			//If service create record successfully, redirect to "Index" action
			_mockService.Setup(ser => ser.AddPerson(_personMock.Object)).Returns(true);
			var expectedAction = "Index";
			//Act
			var result = _controller.Create(_personMock.Object);

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
			//If service failed to delete, return ProblemDetails
			int id = _personMock.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(_personMock.Object);
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
			//If service successfully delete record, redirect to ConfirmDelete option with deleted record as route value
			int id = _personMock.Object.Id;
			var expectedAction = "ConfirmDelete";
			var expectedRouteValue = _personMock.Object;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(_personMock.Object);
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
			//This method don't need input

			//Act
			var result = _controller.ConfirmDelete(_personMock.Object);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(_personMock.Object);
		}

		[Test]
		public void Edit_WithId_ReturnNotFound()
		{
			//Arrange
			// If service cant find record with corresponding Id, reuturn NotFound
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
			//If service found the record with input Id, return view with that record
			int id = _personMock.Object.Id;
			_mockService.Setup(ser => ser.GetPersonById(id)).Returns(_personMock.Object);

			//Act
			var result = _controller.Edit(id);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(_personMock.Object);
		}

		[Test]
		public void Edit_WithValidPerson_ReturnView()
		{
			//Arrange
			//If service failed to edit record, return back to view with current editted record
			_mockService.Setup(ser => ser.EditPerson(_personMock.Object)).Returns(false);

			//Act
			var result = _controller.Edit(_personMock.Object);

			//Assert
			result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(_personMock.Object);
		}

		[Test]
		public void Edit_WithValidPerson_ReturnRedirectToAction()
		{
			//Arrange
			// If service successfully edit record, redirect to index action
			var expectedAction = "Index";
			_mockService.Setup(ser => ser.EditPerson(_personMock.Object)).Returns(true);

			//Act
			var result = _controller.Edit(_personMock.Object);

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
			_mockService.Setup(ser => ser.GetPeople()).Returns(fullnames.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(fullnames.Object.AsQueryable(), 1, fullnames.Object.Count);

			//Act
			var result = _controller.GetFullNames();

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
			_mockService.Setup(ser => ser.GetByBirthYear(input)).Returns(_peopleListMock.Object);
			var paginatedPeopleList = PaginatedList<Person>.Create(_peopleListMock.Object.AsQueryable(), 1, _peopleListMock.Object.Count);
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
