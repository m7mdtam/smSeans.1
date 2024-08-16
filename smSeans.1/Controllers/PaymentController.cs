using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if (payment == null)
            return NotFound();

        return Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateDto paymentDto)
    {
        if (paymentDto == null)
        {
            return BadRequest("Payment data is required.");
        }

        var payment = new Payment
        {
            user_id = paymentDto.user_id,
            amount = paymentDto.amount,
            payment_date = paymentDto.payment_date, // Use the date from DTO
            method = paymentDto.method // Fix: map the 'method' from DTO
        };

        var createdId = await _paymentRepository.CreatePaymentAsync(payment); // Assuming the repo returns created ID

        return CreatedAtAction(nameof(GetPaymentById), new { id = createdId }, payment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentUpdateDto paymentDto)
    {
        if (paymentDto == null || id <= 0)
        {
            return BadRequest("Invalid payment data.");
        }

        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if (payment == null)
        {
            return NotFound();
        }

        // Update the payment details
        payment.amount = paymentDto.amount;
        payment.payment_date = paymentDto.payment_date; // Use the date from DTO
        payment.method = paymentDto.method; // Fix: map the 'method' from DTO

        var updateSuccess = await _paymentRepository.UpdatePaymentAsync(payment);
        if (!updateSuccess)
        {
            return StatusCode(500, "An error occurred while updating the payment.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if (payment == null)
        {
            return NotFound();
        }

        var deleteSuccess = await _paymentRepository.DeletePaymentAsync(id);
        if (!deleteSuccess)
        {
            return StatusCode(500, "An error occurred while deleting the payment.");
        }

        return NoContent();
    }
}
