using Northwind.EntityModels; //to use Customer 

namespace Northwind.WebApi.Repositories;

public interface ICustomerRepository 
{
    Task<Customer?> CreateAsync(Customer C);
    Task<Customer[]> RetrieveAllAsync();
    Task<Customer?> RetrieveAsync(string Id);
    Task<Customer?> UpdateAsync(Customer c);
    Task<bool?> DeleteAsync(string Id);
}
