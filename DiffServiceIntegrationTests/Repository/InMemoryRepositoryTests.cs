using DiffAPI.Models;
using DiffAPI.Repository.InMemory;
using DiffServiceIntegrationTests;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;

namespace DiffService.Repository.Tests
{
    [Category("IntegrationTest")]
    [TestFixture()]
    public class InMemoryRepositoryTests
    {
        private InMemoryRepository _repository;
        private Fixture _fixture;
        private Dictionary<string, Comparison> mockValues;

        [OneTimeSetUp]
        public void CreateSUT()
        {
            _fixture = new Fixture();
            mockValues = _fixture.CreateMany<KeyValuePair<string, Comparison>>()
                .ToDictionary(x => x.Key, x => x.Value);
            _repository = new InMemoryRepository(mockValues);
        }

        [OneTimeTearDown]
        public void FreeSUT()
        {
            _repository = null;
            _fixture = null;
        }

        [Test()]
        public void Given_Valid_Data_When_Insert_Data_Should_Not_Throw_Exception()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64string = _fixture.Create<string>().Base64Encode();
            var comparison = new Comparison() { ComparisonId = id, Left = base64string, Right = base64string };
            //Act & Assert
            Assert.DoesNotThrow(() => _repository.Insert(comparison));
        }

        [Test()]
        public void Given_Valid_Data_When_Insert_Data_Should_Be_Saved()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64string = _fixture.Create<string>().Base64Encode();
            var comparison = new Comparison() { ComparisonId = id, Left = base64string, Right = base64string };
            //Act

            _repository.Insert(comparison);

            //Assert
            var saved = _repository.Get(comparison.ComparisonId);
            Assert.IsNotNull(saved);
            Assert.AreEqual(comparison.Left, saved.Left);
            Assert.AreEqual(comparison.Right, saved.Right);
        }

        [Test()]
        public void Given_Id_That_Exists_When_Get_Data_Then_Should_Return_Value()
        {
            //Arrange
            var id = mockValues.FirstOrDefault().Key;
            var comparison = mockValues.FirstOrDefault().Value;

            //Act
            var ret = _repository.Get(id);

            //Assert
            Assert.IsNotNull(ret);
            Assert.AreEqual(comparison, ret);
        }

        [Test()]
        public void Given_Id_That_Not_Exists_When_Get_Data_Should_Return_Null()
        {
            //Arrange
            var id = _fixture.Create<string>();

            //Act
            //We send the base64string instead of the id, to cause a Not Found
            var ret = _repository.Get(id);

            //Assert
            Assert.IsNull(ret);
        }

        [Test()]
        public void Given_Valid_Data_When_Update_Right_Should_Update()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64string = _fixture.Create<string>().Base64Encode();
            var comparison = new Comparison() { ComparisonId = id, Left = base64string, Right = base64string };
            _repository.Insert(comparison);
            comparison.Right = _fixture.Create<string>().Base64Encode();

            //Act
            _repository.Update(comparison);

            //Assert
            var saved = _repository.Get(comparison.ComparisonId);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.Right, comparison.Right);
        }

        [Test()]
        public void Given_Valid_Data_When_Update_Left_Should_Update()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64string = _fixture.Create<string>().Base64Encode();
            var comparison = new Comparison() { ComparisonId = id, Left = base64string, Right = base64string };
            _repository.Insert(comparison);
            comparison.Left = _fixture.Create<string>().Base64Encode();

            //Act
            _repository.Update(comparison);

            //Assert
            var saved = _repository.Get(comparison.ComparisonId);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.Left, comparison.Left);
        }
    }
}