using FluentAssertions;
using Moq;
using NashTechAssignmentDay5.Application.Interfaces;
using NashTechAssignmentDay5.Application.Services;
using NashTechAssignmentDay5.Domain.Entities;
using NashTechAssignmentDay5.Domain.Enum;

namespace NashTechAssignmentUTestDay1.Test.Services
{
	[TestFixture]
	public class RookiesServiceTest
	{
		private IRookiesService _service;
		private	Mock<IPersonRepository> _mockRepository;
		private Mock<Person> _personMock;
		private Mock<List<Person>> _peopleListMock;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mockRepository = new Mock<IPersonRepository>(MockBehavior.Strict);
			_service = new RookiesService(_mockRepository.Object);
		}

		[SetUp]
		public void Setup()
		{
			_personMock = new Mock<Person>();
			_peopleListMock = new Mock<List<Person>>();
		}

		[Test]
		public void GetPeople_With3People_ReturnListOf3()
		{
			//Arrange
			_mockRepository.Setup(repo => repo.GetAll())
				.Returns(_peopleListMock.Object);
			//Act
			var result = _service.GetPeople();

			//Assert
			result.Should().BeEquivalentTo(_peopleListMock.Object);
		}

		[Test]
		public void GetPersonById_WithAPerson_ReturnAPerson()
		{
			//Arrange
			_mockRepository.Setup(repo => repo.FindById(_personMock.Object.Id))
				.Returns(_personMock.Object);

			//Act
			var result = _service.GetPersonById(_personMock.Object.Id);

			//Assert
			result.Should().BeEquivalentTo(_personMock.Object);
		}

		[Test]
		public void EditPerson_WithAPerson_ReturnTrue()
		{
			//Arrange
			
			_mockRepository.Setup(repo => repo.Update(_personMock.Object))
				.Returns(true);

			//Act
			var result = _service.EditPerson(_personMock.Object);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void EditPerson_WithAPerson_ReturnFalse()
		{
			//Arrange
			
			_mockRepository.Setup(repo => repo.Update(_personMock.Object))
				.Returns(false);

			//Act
			var result = _service.EditPerson(_personMock.Object);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void DeletePerson_WithAPerson_ReturnTrue()
		{
			//Arrange
			
			var id = _personMock.Object.Id;
			_mockRepository.Setup(repo => repo.FindById(id)).Returns(_personMock.Object);
			_mockRepository.Setup(repo => repo.Delete(_personMock.Object)).Returns(true);

			//Act
			var result = _service.DeletePerson(id);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void DeletePerson_WithAPerson_ReturnFalse()
		{
			//Arrange
			Person? person = null;
			var id = 1;
			_mockRepository.Setup(repo => repo.FindById(id)).Returns(person);
			_mockRepository.Setup(repo => repo.Delete(person)).Returns(false);

			//Act
			var result = _service.DeletePerson(id);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void AddPerson_WithAPerson_ReturnTrue()
		{
			//Arrange
			
			_mockRepository.Setup(repo => repo.Create(_personMock.Object))
				.Returns(true);

			//Act
			var result = _service.AddPerson(_personMock.Object);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void AddPerson_WithAPerson_ReturnFalse()
		{
			//Arrange
			
			_mockRepository.Setup(repo => repo.Create(_personMock.Object))
				.Returns(false);

			//Act
			var result = _service.AddPerson(_personMock.Object);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void GetMales_ReturnMales()
		{
			//Arrange
			_mockRepository.Setup(repo => repo.GetMales())
				.Returns(_peopleListMock.Object);

			//Act
			var result = _service.GetMales();

			//Assert
			result.Should().BeEquivalentTo(_peopleListMock.Object);
		}

		[Test]
		public void GetOldest_ReturnPerson()
		{
			//Arrange
			
			_mockRepository.Setup(repo => repo.GetOldest()).Returns(_personMock.Object);

			//Act
			var result = _service.GetOldest();

			//Assert
			result.Should().BeEquivalentTo(_personMock.Object);
		}

		[Test]
		public void GetByBirthYear_WithInput1_ReturnPeople()
		{
			//Arrange
			var input = 1;
			_mockRepository.Setup(repo => repo.FindByBirthYear(input))
				.Returns(_peopleListMock.Object);

			//Act
			var result = _service.GetByBirthYear(input);

			//Assert
			result.Should().BeEquivalentTo(_peopleListMock.Object);
		}

		[Test]
		public void GetByBirthYear_WithInput2_ReturnPeople()
		{
			//Arrange
			var input = 2;
			_mockRepository.Setup(repo => repo.FindByBirthYear(input))
				.Returns(_peopleListMock.Object);

			//Act
			var result = _service.GetByBirthYear(input);

			//Assert
			result.Should().BeEquivalentTo(_peopleListMock.Object);
		}

		[Test]
		public void GetByBirthYear_WithInput3_ReturnPeople()
		{
			//Arrange
			var input = 3;
			_mockRepository.Setup(repo => repo.FindByBirthYear(input))
				.Returns(_peopleListMock.Object);

			//Act
			var result = _service.GetByBirthYear(input);

			//Assert
			result.Should().BeEquivalentTo(_peopleListMock.Object);
		}

		[Test]
		public void ExportToExcel_ReturnTrue()
		{
			//Arrange
			string? path = null;
			_mockRepository.Setup(repo => repo.GetAll()).Returns(_peopleListMock.Object);

			//Act
			var result = _service.ExportToExcel(path);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void ExportToExcel_ReturnFalse()
		{
			//Arrange
			var exception = new Mock<Exception>();
			string? path = null;
			_mockRepository.Setup(repo => repo.GetAll()).Throws(exception.Object);

			//Act
			var result = _service.ExportToExcel(path);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void ExportToExcel_WithPath_ReturnFalse()
		{
			//Arrange
			var exception = new Mock<Exception>();
			string? path = "D:/";
			_mockRepository.Setup(repo => repo.GetAll()).Throws(exception.Object);

			//Act
			var result = _service.ExportToExcel(path);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void ExportToExcel_WithPath_ReturnTrue()
		{
			//Arrange
			string? path = "D:/";
			_mockRepository.Setup(repo => repo.GetAll()).Returns(_peopleListMock.Object);

			//Act
			var result = _service.ExportToExcel(path);

			//Assert
			result.Should().BeTrue();
		}

	}
}
