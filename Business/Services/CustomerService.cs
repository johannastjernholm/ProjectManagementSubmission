using Business.Factories;
using Business.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        //Om kundnamnet finns så lägger vi inte till den
        var existingCustomer = await _customerRepository.GetAsync(x => x.CustomerEmail == form.CustomerEmail);
        if (existingCustomer != null)
        {
            return false;
        }

        var customerEntity = CustomerFactory.Create(form);
        //Kontroll om vi får all indata från formuläret
        if (customerEntity == null)
        {
            throw new Exception("Misslyckades med att skapa kund");
        }
        //Spara entiteten till databasen
        await _customerRepository.AddAsync(customerEntity!);
        return true;
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

    public async Task<Customer?> GetCustomerWithProjectsAsync(int customerId)
    {
        var customerEntity = await _customerRepository.GetQueryable()
            .Include(x => x.Projects)
            .FirstOrDefaultAsync(x => x.Id == customerId);

        if (customerEntity == null)
        { return null; }

        return CustomerFactory.Create(customerEntity);

    }
    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
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
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        if (customerEntity == null)
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
