using AutoFixture;
using customer.form.api.Models;
using customer.form.api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO.Abstractions;

namespace customer.form.api.tests
{
    public class SaveCustomerTests
    {
        private readonly Mock<ILogger<SaveCustomer>> _mLogger;
        private readonly CustomerSettings _customerSettings;

        private SaveCustomer SaveCustomerService { get; set; }
        private readonly Fixture _fixture;

        public SaveCustomerTests()
        {
            _mLogger = new Mock<ILogger<SaveCustomer>>();
            _customerSettings = new CustomerSettings { TextFilePath = "C:/Users/Kawindu.Lokuge/Source/jsondata.txt" };
            _fixture = new Fixture();

            _mLogger.Setup(x => x.LogDebug(It.IsAny<string>())).Verifiable();
            _mLogger.Setup(x => x.LogInformation(It.IsAny<string>())).Verifiable();
            SaveCustomerService = new SaveCustomer(Microsoft.Extensions.Options.Options.Create(_customerSettings), _mLogger.Object);
        }

        [Fact(DisplayName = "Service should throw Exception if null pass in")]
        public void ServiceTests_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SaveCustomer(null, null));
            Assert.Throws<ArgumentNullException>(() => new SaveCustomer(Microsoft.Extensions.Options.Options.Create(_customerSettings), null));
            Assert.Throws<ArgumentNullException>(() => new SaveCustomer(null, _mLogger.Object));
        }

        [Fact]
        public void SaveCustomerDataTextFileAsyncTests_ReturnOk()
        {
            //Arrange
            var path = @"C:/Users/Kawindu.Lokuge/Source/jsondata.txt";
            var customer = _fixture.Create<Customer>();

            var mockFileStream = new Mock<IFileSystem>();
            mockFileStream.Setup(t => t.File.Create(path)).Returns(It.IsAny<Stream>());

            // Act
            var response = SaveCustomerService.SaveCustomerDataTextFileAsync(customer);

            // Assert
            _mLogger.Verify(x => x.LogInformation(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
