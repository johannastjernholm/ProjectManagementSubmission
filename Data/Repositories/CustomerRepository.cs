using Data.DataContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context)
{

    //Create
    public CustomerEntity CreateCustomer(CustomerEntity customerEntity)
    {
        context.Customers.Add(customerEntity);
        context.SaveChanges();

        return customerEntity;
    }

    //Read
    public IEnumerable<CustomerEntity> GetAllCustomers()
    {
        return context.Customers.ToList();
    }

    //Update
    public CustomerEntity UpdateCustomer(CustomerEntity customerEntity)
    {
        context.Customers.Update(customerEntity);
        context.SaveChanges();
        return customerEntity;
    }


    //Delete
    public async Task<bool>  DeleteAsync(int id)
    {
        var entity = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            context.Customers.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

}
