using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
[Authorize]

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(int id)
    {
        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
        if (appointment == null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto appointmentDto)
    {
        if (appointmentDto == null)
        {
            return BadRequest("Appointment data is required.");
        }

        var appointment = new Appointment
        {
            member_id = appointmentDto.member_id,
            assistant_id = appointmentDto.assistant_id,
            appointment_date = appointmentDto.appointment_date,
            status = appointmentDto.status,
            created_at = DateTime.UtcNow
        };

        await _appointmentRepository.CreateAppointmentAsync(appointment);

        return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentUpdateDto appointmentDto)
    {
        if (appointmentDto == null || id <= 0)
        {
            return BadRequest("Invalid appointment data.");
        }

        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        appointment.member_id = appointmentDto.member_id;
        appointment.assistant_id = appointmentDto.assistant_id;
        appointment.appointment_date = appointmentDto.appointment_date;
        appointment.status = appointmentDto.status;

        await _appointmentRepository.UpdateAppointmentAsync(appointment);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        await _appointmentRepository.DeleteAppointmentAsync(id);

        return NoContent();
    }
}
