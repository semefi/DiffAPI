using Dapper;
using DiffAPI.Models;
using DiffAPI.Repository.SQLServer;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Configuration;
using System.Data.SqlClient;

namespace DiffServiceIntegrationTests.Repository
{
    [Category("IntegrationTest")]
    [TestFixture]
    public class ComparisonRepositoryTest
    {
        private IComparisonRepository _repository;
        private Fixture _fixture;
        private string _connectionString;

        [OneTimeSetUp]
        public void CreateSUT()
        {
            _fixture = new Fixture();
            _connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            _repository = new ComparisonRepository(_connectionString);
        }

        [OneTimeTearDown]
        public void FreeSUT()
        {
            _repository = null;
            _fixture = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("TRUNCATE TABLE Comparison");
            }
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

        [Test]
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
            var id = _fixture.Create<string>();
            var base64string = _fixture.Create<string>().Base64Encode();
            var comparison = new Comparison() { ComparisonId = id, Left = base64string, Right = base64string };
            _repository.Insert(comparison);

            //Act
            var ret = _repository.Get(id);

            //Assert
            Assert.IsNotNull(ret);
            Assert.AreEqual(comparison.Left, ret.Left);
            Assert.AreEqual(comparison.Right, ret.Right);
        }

        [Test()]
        public void Given_Id_That_Not_Exists_When_Get_Data_Should_Return_Null()
        {
            //Arrange
            var id = _fixture.Create<string>();

            //Act
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
