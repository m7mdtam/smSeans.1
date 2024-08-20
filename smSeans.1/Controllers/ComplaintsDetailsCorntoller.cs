using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
[Authorize]

[Route("api/[controller]")]
[ApiController]
public class ComplaintDetailsController : ControllerBase
{
    private readonly IComplaintDetailsRepository _complaintDetailsRepository;

    public ComplaintDetailsController(IComplaintDetailsRepository complaintDetailsRepository)
    {
        _complaintDetailsRepository = complaintDetailsRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComplaintDetailsById(int id)
    {
        var complaintDetails = await _complaintDetailsRepository.GetComplaintDetailsByIdAsync(id);
        if (complaintDetails == null)
            return NotFound();

        return Ok(complaintDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComplaintDetails([FromBody] ComplaintDetailsCreateDto complaintDetailsDto)
    {
        if (complaintDetailsDto == null)
        {
            return BadRequest("ComplaintDetails data is required.");
        }

        var complaintDetails = new ComplaintDetails
        {
            complaintId = complaintDetailsDto.complaintId,
            complaintExplanation = complaintDetailsDto.complaintExplanation,
            isImportant = complaintDetailsDto.isImportant,
            isArchived = complaintDetailsDto.isArchived,
            status = complaintDetailsDto.status,
            solution = complaintDetailsDto.solution,
            timeToBeSolved = complaintDetailsDto.timeToBeSolved
        };

        var newId = await _complaintDetailsRepository.CreateComplaintDetailsAsync(complaintDetails);

        return CreatedAtAction(nameof(GetComplaintDetailsById), new { id = newId }, complaintDetails);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComplaintDetails(int id, [FromBody] ComplaintDetailsUpdateDto complaintDetailsDto)
    {
        if (complaintDetailsDto == null || id <= 0)
        {
            return BadRequest("Invalid ComplaintDetails data.");
        }

        var existingComplaintDetails = await _complaintDetailsRepository.GetComplaintDetailsByIdAsync(id);
        if (existingComplaintDetails == null)
        {
            return NotFound();
        }

        existingComplaintDetails.complaintId = complaintDetailsDto.complaintId;
        existingComplaintDetails.complaintExplanation = complaintDetailsDto.complaintExplanation;
        existingComplaintDetails.isImportant = complaintDetailsDto.isImportant;
        existingComplaintDetails.isArchived = complaintDetailsDto.isArchived;
        existingComplaintDetails.status = complaintDetailsDto.Status;
        existingComplaintDetails.solution = complaintDetailsDto.Solution;
        existingComplaintDetails.timeToBeSolved = complaintDetailsDto.timeToBeSolved;

        var updateResult = await _complaintDetailsRepository.UpdateComplaintDetailsAsync(existingComplaintDetails);

        if (!updateResult)
        {
            return BadRequest("Error updating complaint details.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComplaintDetails(int id)
    {
        var existingComplaintDetails = await _complaintDetailsRepository.GetComplaintDetailsByIdAsync(id);
        if (existingComplaintDetails == null)
        {
            return NotFound();
        }

        var deleteResult = await _complaintDetailsRepository.DeleteComplaintDetailsAsync(id);

        if (!deleteResult)
        {
            return BadRequest("Error deleting complaint details.");
        }

        return NoContent();
    }
}
