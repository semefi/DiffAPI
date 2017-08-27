using DiffAPI.Models;
using DiffAPI.Repository.SQLServer;
using DiffAPI.Services;
using DiffServiceIntegrationTests;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Ploeh.AutoFixture;
using System;

namespace DiffServiceTests.Services
{
    /// <summary>
    /// Summary description for ComparisonServiceTests
    /// </summary>
    [TestFixture()]
    public class ComparisonServiceTests
    {
        private Fixture _fixture;

        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _fixture = null;
        }

        [Test]
        public void Given_New_Valid_Data_When_AddLeft_Then_Should_Save_Or_Update()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns((Comparison)null)
                    .Verifiable();
            repoMock.Setup(x => x.Insert(It.IsAny<Comparison>()))
                    .Verifiable();
            repoMock.Setup(x => x.Update(It.IsAny<Comparison>()))
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act
            service.AddLeft(id, base64String);

            //Assert
            //Update must not be execute
            repoMock.Verify(x => x.Update(It.IsNotNull<Comparison>()), Times.Never);
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            repoMock.Verify(x => x.Insert(It.IsNotNull<Comparison>()), Times.Once);
        }

        [Test]
        public void Given_Existing_Valid_Data_When_AddLeft_Then_Should_Save_Or_Update()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(new Comparison() { ComparisonId = id, Right = base64String })
                    .Verifiable();
            repoMock.Setup(x => x.Insert(It.IsAny<Comparison>()))
                    .Verifiable();
            repoMock.Setup(x => x.Update(It.IsAny<Comparison>()))
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act
            service.AddLeft(id, base64String);

            //Assert
            //Insert must not be execute
            repoMock.Verify(x => x.Insert(It.IsNotNull<Comparison>()), Times.Never);
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            repoMock.Verify(x => x.Update(It.IsNotNull<Comparison>()), Times.Once);
        }

        [Test]
        public void Given_New_Valid_Data_When_AddRight_Then_Should_Save_Or_Update()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns((Comparison)null)
                    .Verifiable();
            repoMock.Setup(x => x.Insert(It.IsAny<Comparison>()))
                    .Verifiable();
            repoMock.Setup(x => x.Update(It.IsAny<Comparison>()))
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act
            service.AddRight(id, base64String);

            //Assert
            //Update must not be execute
            repoMock.Verify(x => x.Update(It.IsNotNull<Comparison>()), Times.Never);
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            repoMock.Verify(x => x.Insert(It.IsNotNull<Comparison>()), Times.Once);

        }

        [Test]
        public void Given_Existing_Valid_Data_When_AddRight_Then_Should_Save_Or_Update()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(new Comparison() { ComparisonId = id, Left = base64String })
                    .Verifiable();
            repoMock.Setup(x => x.Insert(It.IsAny<Comparison>()))
                    .Verifiable();
            repoMock.Setup(x => x.Update(It.IsAny<Comparison>()))
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act
            service.AddRight(id, base64String);

            //Assert
            //Insert must not be execute
            repoMock.Verify(x => x.Insert(It.IsNotNull<Comparison>()), Times.Never);
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            repoMock.Verify(x => x.Update(It.IsNotNull<Comparison>()), Times.Once);

        }

        [Test]
        public void Given_Invalid_Data_When_AddLeft_Then_Should_Return_Exception()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = "";

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns((Comparison)null)
                    .Verifiable();
            repoMock.Setup(x => x.Insert(It.IsAny<Comparison>()))
                    .Verifiable();
            repoMock.Setup(x => x.Update(It.IsAny<Comparison>()))
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddLeft(id, base64String));
            repoMock.Verify(x => x.Insert(It.IsNotNull<Comparison>()), Times.Never);
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Never);
            repoMock.Verify(x => x.Update(It.IsNotNull<Comparison>()), Times.Never);

        }

        [Test]
        public void Given_Invalid_Data_When_AddRight_Then_Should_Return_Exception()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = "";

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns((Comparison)null)
                    .Verifiable();
            repoMock.Setup(x => x.Insert(It.IsAny<Comparison>()))
                    .Verifiable();
            repoMock.Setup(x => x.Update(It.IsAny<Comparison>()))
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddRight(id, base64String));
            repoMock.Verify(x => x.Insert(It.IsNotNull<Comparison>()), Times.Never);
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Never);
            repoMock.Verify(x => x.Update(It.IsNotNull<Comparison>()), Times.Never);

        }

        [Test]
        public void Given_Valid_Id_When_Execute_Get_Then_Should_Return_Comparison()
        {
            //Arrange
            var id = _fixture.Create<string>();
            var base64String = _fixture.Create<string>().Base64Encode();
            var comparison = new Comparison() { ComparisonId = id, Left = base64String, Right = base64String };

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns(comparison)
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act
            var result = service.Get(id);

            //Assert
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(comparison.ComparisonId, result.ComparisonId);
            Assert.AreEqual(comparison.Left, result.Left);
            Assert.AreEqual(comparison.Right, result.Right);
        }

        [Test]
        public void Given_Invalid_Id_When_Execute_Get_Then_Should_Return_Exception()
        {
            //Arrange
            var id = "";

            var repoMock = new Mock<IComparisonRepository>();
            repoMock.Setup(x => x.Get(It.IsNotNull<string>()))
                    .Returns((Comparison)null)
                    .Verifiable();

            var service = new ComparisonService(repoMock.Object);


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.Get(id));
            repoMock.Verify(x => x.Get(It.IsNotNull<string>()), Times.Never);
        }
    }
}
