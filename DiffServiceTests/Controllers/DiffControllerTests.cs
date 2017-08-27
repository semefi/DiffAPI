using DiffAPI.Controllers;
using DiffAPI.Models;
using DiffAPI.Services;
using DiffAPI.ViewModel;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DiffService.Controllers.Tests
{
    [TestFixture()]
    public class DiffControllerTests
    {
        private Fixture _fixture;

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            _fixture = new Fixture();
        }

        [OneTimeTearDown]
        public void TeardDown()
        {
            _fixture = null;
        }

        [Test()]
        public void Given_Valid_Data_When_Execute_Left_Then_Should_Return_Ok()
        {
            //Arrange
            var diffServiceMock = new Mock<IDiffService>();
            var comparisonService = new Mock<IComparisonService>();
            comparisonService.Setup(x => x.AddLeft(It.IsNotNull<string>(), It.IsNotNull<string>())).Verifiable();
            var controller = new DiffController(diffServiceMock.Object, comparisonService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>();

            //Act
            var response = controller.Left(id, base64String);

            //Assert
            comparisonService.Verify(x => x.AddLeft(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test()]
        public void Given_Invalid_Base64Data_When_Execute_Left_Then_Should_Return_Bad_Request()
        {
            //Arrange
            var diffServiceMock = new Mock<IDiffService>();
            var comparisonService = new Mock<IComparisonService>();
            comparisonService.Setup(x => x.AddLeft(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Throws(new ArgumentNullException("data"))
                .Verifiable();
            var controller = new DiffController(diffServiceMock.Object, comparisonService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration(),
            };

            var id = _fixture.Create<string>();
            var base64String = "";

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => controller.Left(id, base64String));
            comparisonService.Verify(x => x.AddLeft(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
        }

        [Test()]
        public void Given_Invalid_ID_When_Execute_Left_Then_Should_Return_Bad_Request()
        {
            //Arrange
            var diffServiceMock = new Mock<IDiffService>();
            var comparisonService = new Mock<IComparisonService>();
            comparisonService.Setup(x => x.AddLeft(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Throws(new ArgumentNullException("data"))
                .Verifiable();
            var controller = new DiffController(diffServiceMock.Object, comparisonService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = "";
            var base64String = _fixture.Create<string>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => controller.Left(id, base64String));
            comparisonService.Verify(x => x.AddLeft(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
        }

        [Test()]
        public void Given_Valid_Data_When_Execute_Right_Then_Should_Return_Ok()
        {
            //Arrange
            var diffServiceMock = new Mock<IDiffService>();
            var comparisonService = new Mock<IComparisonService>();
            comparisonService.Setup(x => x.AddRight(It.IsNotNull<string>(), It.IsNotNull<string>())).Verifiable();
            var controller = new DiffController(diffServiceMock.Object, comparisonService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>();

            //Act
            var response = controller.Right(id, base64String);

            //Assert
            comparisonService.Verify(x => x.AddRight(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test()]
        public void Given_Invalid_Base64Data_When_Execute_Right_Then_Should_Return_Bad_Request()
        {
            //Arrange
            var diffServiceMock = new Mock<IDiffService>();
            var comparisonService = new Mock<IComparisonService>();
            comparisonService.Setup(x => x.AddRight(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Throws(new ArgumentNullException("data"))
                .Verifiable();
            var controller = new DiffController(diffServiceMock.Object, comparisonService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = _fixture.Create<string>();
            var base64String = "";

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => controller.Right(id, base64String));
            comparisonService.Verify(x => x.AddRight(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
        }

        [Test()]
        public void Given_Invalid_ID_When_Execute_Right_Then_Should_Return_Bad_Request()
        {
            //Arrange
            var diffServiceMock = new Mock<IDiffService>();
            var comparisonService = new Mock<IComparisonService>();
            comparisonService.Setup(x => x.AddRight(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Throws(new ArgumentNullException("data"))
                .Verifiable();
            var controller = new DiffController(diffServiceMock.Object, comparisonService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = "";
            var base64String = _fixture.Create<string>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => controller.Right(id, base64String));
            comparisonService.Verify(x => x.AddRight(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
        }

        [Test()]
        public void Given_Valid_And_Equal_Data_When_Execute_Diff_Then_Should_Return_AreEqual()
        {
            //Arrange
            var base64String = _fixture.Create<string>();
            var differences = _fixture.Build<Result>()
                .With(x => x.AreEqual, true).With(x => x.AreSameSize, true)
                .With(x => x.Differences, new List<Difference>()).Create();

            var diffServiceMock = new Mock<IDiffService>();
            diffServiceMock.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(differences);

            var repositoryMock = new Mock<IComparisonService>();
            repositoryMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(new Comparison() { Left = base64String, Right = base64String })
                    .Verifiable();

            var controller = new DiffController(diffServiceMock.Object, repositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = _fixture.Create<string>();

            //Act
            var response = controller.Diff(id);


            //Assert
            repositoryMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            diffServiceMock.Verify(x => x.GetDifferences(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Result retVal;
            Assert.IsTrue(response.TryGetContentValue(out retVal));
            Assert.IsTrue(retVal.AreEqual);
            Assert.IsTrue(retVal.AreSameSize);
            CollectionAssert.IsEmpty(retVal.Differences);
        }

        [Test()]
        public void Given_Valid_And_DiffSized_When_Execute_Diff_Then_Should_Return_AreDiffSize()
        {
            //Arrange
            var base64String = _fixture.Create<string>();
            var differences = _fixture.Build<Result>()
                .With(x => x.AreEqual, false).With(x => x.AreSameSize, false)
                .With(x => x.Differences, new List<Difference>()).Create();

            var diffServiceMock = new Mock<IDiffService>();
            diffServiceMock.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(differences);

            var repositoryMock = new Mock<IComparisonService>();
            repositoryMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(new Comparison() { Left = base64String.Substring(0, base64String.Length - 2), Right = base64String })
                    .Verifiable();

            var controller = new DiffController(diffServiceMock.Object, repositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var id = _fixture.Create<string>();

            //Act
            var response = controller.Diff(id);


            //Assert
            repositoryMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            diffServiceMock.Verify(x => x.GetDifferences(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Result retVal;
            Assert.IsTrue(response.TryGetContentValue(out retVal));
            Assert.IsTrue(!retVal.AreEqual);
            Assert.IsTrue(!retVal.AreSameSize);
            CollectionAssert.IsEmpty(retVal.Differences);
        }

        [Test()]
        public void Given_Valid_And_Different_Data_When_Execute_Diff_Then_Should_Return_Differences()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>();
            var base64String2 = base64String.Replace(base64String.Substring(2, 1), base64String.Substring(4, 1));
            var differences = _fixture.Build<Result>()
                .With(x => x.AreEqual, false).With(x => x.AreSameSize, true)
                .With(x => x.Differences, _fixture.CreateMany<Difference>().ToArray()).Create();

            var diffServiceMock = new Mock<IDiffService>();
            diffServiceMock.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(differences);

            var comparisonServiceMock = new Mock<IComparisonService>();
            comparisonServiceMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(new Comparison() { ComparisonId = id, Left = base64String, Right = base64String2 })
                    .Verifiable();

            var controller = new DiffController(diffServiceMock.Object, comparisonServiceMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };


            //Act
            var response = controller.Diff(id);


            //Assert
            comparisonServiceMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            diffServiceMock.Verify(x => x.GetDifferences(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Result retVal;
            Assert.IsTrue(response.TryGetContentValue(out retVal));
            Assert.IsTrue(!retVal.AreEqual);
            Assert.IsTrue(retVal.AreSameSize);
            CollectionAssert.AllItemsAreUnique(retVal.Differences);
            Assert.Greater(retVal.Differences.Count, 0);
        }

        [Test()]
        public void Given_Id_Incomplete_Left_And_Right_Data_When_Execute_Diff_Then_Should_Return_Bad_Request()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>();

            var diffServiceMock = new Mock<IDiffService>();
            diffServiceMock.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var comparisonServiceMock = new Mock<IComparisonService>();
            comparisonServiceMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(new Comparison() { ComparisonId = id, Left = base64String, Right = "" })
                    .Verifiable();

            var controller = new DiffController(diffServiceMock.Object, comparisonServiceMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };


            //Act
            var response = controller.Diff(id);


            //Assert
            comparisonServiceMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            diffServiceMock.Verify(x => x.GetDifferences(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Never);
            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}