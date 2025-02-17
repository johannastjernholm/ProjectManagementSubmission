using Business.Factories;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;


    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var customerEntity = CustomerFactory.Create(form);

        await _customerRepository.AddAsync(customerEntity!);
    }

    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);


    }
    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);

    }
    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName) 
    { 
        var customerEntity = await _customerRepository.GetAsync(x=> x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!);
    
    
    
    }
    public async Task<bool> UpdateCustomerAsync(Customer customer) 
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == customer.Id);
        if (customerEntity != null)
        {
            customerEntity.CustomerName = customer.CustomerName;
            await _customerRepository.UpdateAsync(customerEntity);
            return true;
            
        }
        else
        {
            return false;
        }   
    
    
    }
    public async Task<bool> DeleteCustomerAsync(int id) 
    {
        var customerEntity = await _customerRepository.GetAsync( x => x.Id == id);
        if ( customerEntity == null)
        {
            return false;
        }
        else 
        {
            await _customerRepository.RemoveAsync(customerEntity);
            return true;
        }
    }



}
