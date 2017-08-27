using Dapper;
using DiffAPI;
using DiffAPI.Repository.SQLServer;
using DiffAPI.ViewModel;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiffServiceIntegrationTests.Services
{
    [Category("IntegrationTest")]
    [TestFixture]
    public class DiffServiceIntegrationTests
    {
        private TestServer _server;
        private IComparisonRepository _repository;
        private Fixture _fixture;
        private string _connectionString;

        [SetUp]
        public void SetupTest()
        {
            _server = TestServer.Create<Startup>();
            _fixture = new Fixture();
            _connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            _repository = new ComparisonRepository(_connectionString);
        }

        [TearDown]
        public void DisposeTest()
        {
            _server.Dispose();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("TRUNCATE TABLE Comparison");
            }
        }


        [Test]
        public async Task Given_Valid_Data_When_Call_Left_Method_Then_Return_Ok_And_Save()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();
            var endPoint = $"v1/diff/{id}/left";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", base64String)
            });

            //Act
            var response = await _server.HttpClient.PostAsync(endPoint, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string text = await response.Content.ReadAsStringAsync();
            Assert.Greater(text.Length, 0);
            Assert.AreEqual("Saved Left Side", text);
        }

        [Test]
        public async Task Given_Valid_Data_When_Call_Right_Method_Then_Should_Return_Ok_And_Save()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();
            var endPoint = $"v1/diff/{id}/right";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", base64String)
            });

            //Act
            var response = await _server.HttpClient.PostAsync(endPoint, content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string text = await response.Content.ReadAsStringAsync();
            Assert.Greater(text.Length, 0);
            Assert.AreEqual("Saved Right Side", text);
        }

        [Test]
        public async Task Given_Valid_And_Equal_Data_When_Call_All_Methods_Then_Should_Return_Are_Equal()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();

            var rightEndPoint = $"v1/diff/{id}/right";
            var leftEndPoint = $"v1/diff/{id}/left";
            var diffEndPoint = $"v1/diff/{id}";
            var contentLeft = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", base64String)
            });
            var contentRight = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", base64String)
            });

            //Act
            var responseLeft = await _server.HttpClient.PostAsync(leftEndPoint, contentLeft);

            var responseRight = await _server.HttpClient.PostAsync(rightEndPoint, contentRight);

            var responseDiff = await _server.HttpClient.GetAsync(diffEndPoint);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, responseLeft.StatusCode);
            string leftText = await responseLeft.Content.ReadAsStringAsync();
            Assert.Greater(leftText.Length, 0);
            Assert.AreEqual("Saved Left Side", leftText);

            Assert.AreEqual(HttpStatusCode.OK, responseRight.StatusCode);
            string rightText = await responseRight.Content.ReadAsStringAsync();
            Assert.Greater(rightText.Length, 0);
            Assert.AreEqual("Saved Right Side", rightText);

            Assert.AreEqual(HttpStatusCode.OK, responseDiff.StatusCode);
            var content = await responseDiff.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(content);
            Assert.IsTrue(result.AreEqual);
            Assert.IsTrue(result.AreSameSize);
            CollectionAssert.IsEmpty(result.Differences);
        }

        [Test]
        public async Task Given_Valid_And_DiffSized_Data_When_Call_All_Methods_Then_Should_Return_Are_DiffSize()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();
            var base64String2 = base64String + _fixture.Create<string>().Base64Encode();

            var rightEndPoint = $"v1/diff/{id}/right";
            var leftEndPoint = $"v1/diff/{id}/left";
            var diffEndPoint = $"v1/diff/{id}";

            var contentLeft = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", base64String)
            });
            var contentRight = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", base64String2)
            });

            //Act
            var responseLeft = await _server.HttpClient.PostAsync(leftEndPoint, contentLeft);

            var responseRight = await _server.HttpClient.PostAsync(rightEndPoint, contentRight);

            var responseDiff = await _server.HttpClient.GetAsync(diffEndPoint);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, responseLeft.StatusCode);
            string leftText = await responseLeft.Content.ReadAsStringAsync();
            Assert.Greater(leftText.Length, 0);
            Assert.AreEqual("Saved Left Side", leftText);

            Assert.AreEqual(HttpStatusCode.OK, responseRight.StatusCode);
            string rightText = await responseRight.Content.ReadAsStringAsync();
            Assert.Greater(rightText.Length, 0);
            Assert.AreEqual("Saved Right Side", rightText);

            Assert.AreEqual(HttpStatusCode.OK, responseDiff.StatusCode);
            var content = await responseDiff.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(content);
            Assert.IsFalse(result.AreEqual);
            Assert.IsFalse(result.AreSameSize);
            CollectionAssert.IsEmpty(result.Differences);
        }
    }
}
