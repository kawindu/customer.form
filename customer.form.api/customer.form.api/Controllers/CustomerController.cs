using customer.form.api.Models;
using customer.form.api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customer.form.api.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ISaveCustomer _saveCustomer;

        public CustomerController(ILogger<CustomerController> logger, ISaveCustomer saveCustomer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            _saveCustomer = saveCustomer ?? throw new ArgumentNullException(nameof(saveCustomer)); 
        }

        [HttpPost]
        [Route("PostDetails")]
        [SwaggerOperation("PostCustomerDetails")]
        public async Task<IActionResult> PostCustomerDetails([FromBody] Customer customer)
        {
            try
            {
                await _saveCustomer.SaveCustomerDataTextFileAsync(customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while saving customer data", ex);
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}