using customer.form.api.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace customer.form.api.Services
{
    public class SaveCustomer : ISaveCustomer
    {
        private readonly IOptions<CustomerSettings> _options;
        private readonly ILogger<SaveCustomer> _logger;

        public SaveCustomer(IOptions<CustomerSettings> options, ILogger<SaveCustomer> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SaveCustomerDataTextFileAsync(Customer customer)
        {
            if (string.IsNullOrEmpty(_options.Value.TextFilePath))
            {
                throw new Exception("Could not find file path");
            }

            _logger.LogDebug($"File path: {_options.Value.TextFilePath}");

            await using FileStream createStream = File.Create(_options.Value.TextFilePath);
            await JsonSerializer.SerializeAsync(createStream, customer);
            
            _logger.LogInformation("Saving is completed.");
        }
    }
}
