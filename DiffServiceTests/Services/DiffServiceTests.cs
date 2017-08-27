using DiffServiceIntegrationTests;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace DiffService.Services.Tests
{
    [TestFixture()]
    public class DiffServiceTests
    {
        private DiffAPI.Services.DiffService _diffService;
        private Fixture _fixture;

        [OneTimeSetUp]
        public void CreateSUT()
        {
            _fixture = new Fixture();
            _diffService = new DiffAPI.Services.DiffService();
        }

        [OneTimeTearDown]
        public void FreeSUT()
        {
            _diffService = null;
            _fixture = null;
        }

        [Test()]
        public void Compare_Two_String_Are_Same_Length()
        {
            //Arrange
            var base64String = _fixture.Create<string>().Base64Encode();

            //Act
            var result = _diffService.GetDifferences(base64String, base64String);

            //Assert
            Assert.AreEqual(true, result.AreEqual);
            Assert.AreEqual(true, result.AreSameSize);
        }

        [Test()]
        public void Compare_Two_Strings_Are_NOT_Same_Length()
        {
            //Arrange
            var base64String = _fixture.Create<string>().Base64Encode();

            //Act
            var result = _diffService.GetDifferences(base64String.Substring(0, base64String.Length - 2), base64String);

            //Assert
            Assert.AreEqual(false, result.AreEqual);
        }

        [Test()]
        [TestCase("12345", "12445")]
        [TestCase("12345", "11345")]
        [TestCase("12345", "12344")]
        public void Compare_Two_String_Same_Lenght_With_One_Difference(string left, string right)
        {
            //Arrange

            //Act
            var result = _diffService.GetDifferences(left, right);

            //Assert
            Assert.AreEqual(false, result.AreEqual);
            Assert.IsNotNull(result.Differences);
            Assert.AreEqual(1, result.Differences.Count);
        }

        [Test()]
        [TestCase("1234567", "7654321")]
        [TestCase("1234567", "1234577")]
        [TestCase("1234567", "1124557")]
        public void Compare_Two_String_Same_Lenght_With_Multiple_Difference(string left, string right)
        {
            //Arrange

            //Act
            var result = _diffService.GetDifferences(left, right);

            //Assert
            Assert.AreEqual(false, result.AreEqual);
            Assert.IsNotNull(result.Differences);
            Assert.Greater(result.Differences.Count, 0);
        }
    }
}