using Business.Models;
using Data.Entities;

namespace Business.Factories;
//Koden skriven vid Hans Mattin-Lassei föreläsning
public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
        CustomerEmail = form.CustomerEmail

    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
        CustomerEmail = entity.CustomerEmail
    };




}
