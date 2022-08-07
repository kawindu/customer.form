using AutoFixture;
using customer.form.api.Controllers;
using customer.form.api.Models;
using customer.form.api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace customer.form.api.tests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ILogger<CustomerController>> _mLogger;
        private readonly Mock<ISaveCustomer> _mSaveCustomer;

        private CustomerController Controller { get; set; }
        private readonly Fixture _fixture;

        public CustomerControllerTests() {
            _mLogger = new Mock<ILogger<CustomerController>>();
            _mSaveCustomer = new Mock<ISaveCustomer>();
            _fixture = new Fixture();
            Controller = new CustomerController(_mLogger.Object, _mSaveCustomer.Object);
        }

        [Fact(DisplayName = "Constructor should throw Exception if null pass in")]
        public void ConstrutorTests_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerController(null, null));
            Assert.Throws<ArgumentNullException>(() => new CustomerController(_mLogger.Object, null));
            Assert.Throws<ArgumentNullException>(() => new CustomerController(null, _mSaveCustomer.Object));
        }

        [Fact(DisplayName = "PostCustomerDetails returns ok (200)")]
        public void PostCustomerDetailsTest_ReturnOk()
        {
            var customer = _fixture.Create<Customer>();

            // Act
            _mSaveCustomer.Setup(x => x.SaveCustomerDataTextFileAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);
            var response = Controller.PostCustomerDetails(customer);

            // Assert
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact(DisplayName = "PostCustomerDetails returns Exception (500)")]
        public void PostCustomerDetailsTest_ReturnException()
        {
            var customer = _fixture.Create<Customer>();
            _mSaveCustomer.Setup(x => x.SaveCustomerDataTextFileAsync(It.IsAny<Customer>())).ThrowsAsync(new Exception(""));

            // Act
            var response = Controller.PostCustomerDetails(customer);

            // Assert
            var result = Assert.IsType<ObjectResult>(response.Result);
            Assert.Contains("500", result.StatusCode.ToString());
        }
    }
}