using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressRepository _addressRepository;

    public AddressController(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address == null)
            return NotFound();

        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest("Address data is required.");
        }

        var address = new Address
        {
            street = addressDto.street,
            city = addressDto.city,
            state = addressDto.state,
            zip_code = addressDto.zip_code,
            country = addressDto.country
        };

        await _addressRepository.CreateAddressAsync(address);

        return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressUpdateDto addressDto)
    {
        if (addressDto == null || id <= 0)
        {
            return BadRequest("Invalid address data.");
        }

        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        address.street = addressDto.street;
        address.city = addressDto.city;
        address.state = addressDto.state;
        address.zip_code = addressDto.zip_code;
        address.country = addressDto.country;

        await _addressRepository.UpdateAddressAsync(address);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        await _addressRepository.DeleteAddressAsync(id);

        return NoContent();
    }
}
