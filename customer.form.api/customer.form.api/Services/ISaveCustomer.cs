using customer.form.api.Models;

namespace customer.form.api.Services
{
    public interface ISaveCustomer
    {
        Task SaveCustomerDataTextFileAsync(Customer customer);
    }
}
