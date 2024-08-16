using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ComplaintsController : ControllerBase
{
    private readonly IComplaintRepository _complaintsRepository;

    public ComplaintsController(IComplaintRepository complaintsRepository)
    {
        _complaintsRepository = complaintsRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComplaintById(int id)
    {
        var complaint = await _complaintsRepository.GetComplaintByIdAsync(id);
        if (complaint == null)
            return NotFound();

        return Ok(complaint);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComplaint([FromBody] ComplaintsCreateDto complaintsDto)
    {
        if (complaintsDto == null)
        {
            return BadRequest("Complaint data is required.");
        }

        var complaint = new Complaints
        {
            ComplaintType = complaintsDto.ComplaintType,
            user_id = complaintsDto.user_id
        };

        var newComplaintId = await _complaintsRepository.CreateComplaintAsync(complaint);

        // Assuming CreateComplaintAsync returns the created record's ID
        return CreatedAtAction(nameof(GetComplaintById), new { id = newComplaintId }, complaint);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComplaint(int id, [FromBody] ComplaintsUpdateDto complaintsDto)
    {
        if (complaintsDto == null || id <= 0)
        {
            return BadRequest("Invalid complaint data.");
        }

        var existingComplaint = await _complaintsRepository.GetComplaintByIdAsync(id);
        if (existingComplaint == null)
        {
            return NotFound();
        }

        // Update only the fields that are provided
        existingComplaint.ComplaintType = complaintsDto.ComplaintType;
        existingComplaint.user_id = complaintsDto.user_id;

        var updateResult = await _complaintsRepository.UpdateComplaintAsync(existingComplaint);

        if (!updateResult)
        {
            return BadRequest("Error updating complaint.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComplaint(int id)
    {
        var existingComplaint = await _complaintsRepository.GetComplaintByIdAsync(id);
        if (existingComplaint == null)
        {
            return NotFound();
        }

        var deleteResult = await _complaintsRepository.DeleteComplaintAsync(id);

        if (!deleteResult)
        {
            return BadRequest("Error deleting complaint.");
        }

        return NoContent();
    }
}
