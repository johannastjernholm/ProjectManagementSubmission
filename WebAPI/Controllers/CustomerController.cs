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

    

}
