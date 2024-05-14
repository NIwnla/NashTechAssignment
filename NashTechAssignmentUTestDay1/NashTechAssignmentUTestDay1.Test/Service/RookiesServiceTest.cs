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
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mockRepository = new Mock<IPersonRepository>(MockBehavior.Strict);
			_service = new RookiesService(_mockRepository.Object);
		}

		//[Test]
		//public void Mock()
		//{
		//	//Arrange

		//	//Act

		//	//Assert
		//}

		[Test]
		public void GetPeople_With3People_ReturnListOf3()
		{
			//Arrange
			var people = new List<Person>
			{
				new Mock<Person>().Object,
				new Mock<Person>().Object,
				new Mock<Person>().Object
			};
			_mockRepository.Setup(repo => repo.GetAll())
				.Returns(people);
			//Act
			var result = _service.GetPeople();

			//Assert
			result.Should().BeEquivalentTo(people);
		}

		[Test]
		public void GetPersonById_WithAPerson_ReturnAPerson()
		{
			//Arrange
			var person = new Mock<Person>().Object;
			_mockRepository.Setup(repo => repo.FindById(person.Id))
				.Returns(person);

			//Act
			var result = _service.GetPersonById(person.Id);

			//Assert
			result.Should().BeEquivalentTo(person);
		}

		[Test]
		public void EditPerson_WithAPerson_ReturnTrue()
		{
			//Arrange
			var person = new Mock<Person>().Object;
			_mockRepository.Setup(repo => repo.Update(person))
				.Returns(true);

			//Act
			var result = _service.EditPerson(person);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void EditPerson_WithAPerson_ReturnFalse()
		{
			//Arrange
			var person = new Mock<Person>().Object;
			_mockRepository.Setup(repo => repo.Update(person))
				.Returns(false);

			//Act
			var result = _service.EditPerson(person);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void DeletePerson_WithAPerson_ReturnTrue()
		{
			//Arrange
			var person = new Mock<Person>().Object;
			var id = person.Id;
			_mockRepository.Setup(repo => repo.FindById(id)).Returns(person);
			_mockRepository.Setup(repo => repo.Delete(person)).Returns(true);

			//Act
			var result = _service.DeletePerson(id);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void DeletePerson_WithAPerson_ReturnFalse()
		{
			//Arrange
			Person person = null;
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
			var person = new Mock<Person>().Object;
			_mockRepository.Setup(repo => repo.Create(person))
				.Returns(true);

			//Act
			var result = _service.AddPerson(person);

			//Assert
			result.Should().BeTrue();
		}

		[Test]
		public void AddPerson_WithAPerson_ReturnFalse()
		{
			//Arrange
			var person = new Mock<Person>().Object;
			_mockRepository.Setup(repo => repo.Create(person))
				.Returns(false);

			//Act
			var result = _service.AddPerson(person);

			//Assert
			result.Should().BeFalse();
		}

		[Test]
		public void GetMales_ReturnMales()
		{
			//Arrange
			var people = new List<Person>
			{
				new Mock<Person>().Object,
				new Mock<Person>().Object,
				new Mock<Person>().Object
			};
			_mockRepository.Setup(repo => repo.GetMales())
				.Returns(people);

			//Act
			var result = _service.GetMales();

			//Assert
			result.Should().BeEquivalentTo(people);
		}

		[Test]
		public void GetOldest_ReturnPerson()
		{
			//Arrange
			var person = new Mock<Person>().Object;
			_mockRepository.Setup(repo => repo.GetOldest()).Returns(person);

			//Act
			var result = _service.GetOldest();

			//Assert
			result.Should().BeEquivalentTo(person);
		}

		[Test]
		public void GetByBirthYear_WithInput1_ReturnPeople()
		{
			//Arrange
			var input = 1;
			var people = new Mock<List<Person>>();
			_mockRepository.Setup(repo => repo.FindByBirthYear(input))
				.Returns(people.Object);

			//Act
			var result = _service.GetByBirthYear(input);

			//Assert
			result.Should().BeEquivalentTo(people.Object);
		}

		[Test]
		public void GetByBirthYear_WithInput2_ReturnPeople()
		{
			//Arrange
			var input = 2;
			var people = new Mock<List<Person>>();
			_mockRepository.Setup(repo => repo.FindByBirthYear(input))
				.Returns(people.Object);

			//Act
			var result = _service.GetByBirthYear(input);

			//Assert
			result.Should().BeEquivalentTo(people.Object);
		}

		[Test]
		public void GetByBirthYear_WithInput3_ReturnPeople()
		{
			//Arrange
			var input = 3;
			var people = new Mock<List<Person>>();
			_mockRepository.Setup(repo => repo.FindByBirthYear(input))
				.Returns(people.Object);

			//Act
			var result = _service.GetByBirthYear(input);

			//Assert
			result.Should().BeEquivalentTo(people.Object);
		}

		[Test]
		public void ExportToExcel_ReturnTrue()
		{
			//Arrange
			var people = new Mock<List<Person>>();
			string? path = null;
			_mockRepository.Setup(repo => repo.GetAll()).Returns(people.Object);

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

	}
}
