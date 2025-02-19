using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
//Controller skapad med hjälp av ChatGPT

[ApiController]
[Route("api/[controller]")]
public class CustomerController(CustomerService customerService) : ControllerBase
{
    private readonly CustomerService _customerService = customerService;

    /// <summary>
    /// Hämta alla kunder
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _customerService.GetCustomersAsync();
        return Ok(customers);
    }

    /// <summary>
    /// Hämta kund med kundnamn
    /// </summary>
    [HttpGet("{customerName}")]
    public async Task<ActionResult<Customer?>> GetCustomerByCustomerName(string customerName)
    {
        var customer = await _customerService.GetCustomerByCustomerNameAsync(customerName);
        if (customer == null)
        {
            return NotFound($"Ingen kund med {customerName} hittades");
        }
        return Ok(customer);
    }

    /// <summary>
    /// Hämta kund och projekt
    /// </summary>
    [HttpGet("{id}/projects")]
    public async Task<ActionResult<Customer?>> GetCustomerWithProjects(int id)
    {
        var customer = await _customerService.GetCustomerWithProjectsAsync(id);
        if (customer == null)
        {
            return NotFound($"Ingen kund hittades med id: {id}");
        }

        return Ok(customer);
    }

    /// <summary>
    /// Skapa ny kund
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateCustomer([FromBody] CustomerRegistrationForm form)
    {
        if (form == null)
        {
            return BadRequest("Formulärdata saknas");
        }

        var result = await _customerService.CreateCustomerAsync(form);
        if(!result)
        {
            return Conflict("Kunden finns redan");
        }
        return Ok("Ny kund har skapats");
    }
    /// <summary>
    /// Uppdatera en kund
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
    {
        customer.Id = id;

        var result = await _customerService.UpdateCustomerAsync(customer);
        if (!result)
        {
            return NotFound("Kunden finns inte");
        }
        return Ok("Kund uppdaterad");
    }
    /// <summary>
    /// Radera kunden
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (!result)
        {
            return NotFound($"Kunden hittades inte med id: {id}");
        }
        return Ok("Kund raderad");
    }

}
