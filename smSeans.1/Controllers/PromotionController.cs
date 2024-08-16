using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PromotionController : ControllerBase
{
    private readonly IPromotionRepository _promotionRepository;

    public PromotionController(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPromotionById(int id)
    {
        var promotion = await _promotionRepository.GetPromotionByIdAsync(id);
        if (promotion == null)
            return NotFound();

        return Ok(promotion);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotion([FromBody] PromotionCreateDto promotionDto)
    {
        if (promotionDto == null)
        {
            return BadRequest("Promotion data is required.");
        }

        var promotion = new Promotion
        {
            title = promotionDto.title,
            description = promotionDto.description,
            start_date = promotionDto.start_date,
            end_date = promotionDto.end_date,
            user_id = promotionDto.user_id
        };

        var createdId = await _promotionRepository.CreatePromotionAsync(promotion);

        return CreatedAtAction(nameof(GetPromotionById), new { id = createdId }, promotion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePromotion(int id, [FromBody] PromotionUpdateDto promotionDto)
    {
        if (promotionDto == null || id <= 0)
        {
            return BadRequest("Invalid promotion data.");
        }

        var existingPromotion = await _promotionRepository.GetPromotionByIdAsync(id);
        if (existingPromotion == null)
        {
            return NotFound();
        }

        existingPromotion.title = promotionDto.title;
        existingPromotion.description = promotionDto.description;
        existingPromotion.start_date = promotionDto.start_date;
        existingPromotion.end_date = promotionDto.end_date;
        existingPromotion.user_id = promotionDto.user_id;

        var success = await _promotionRepository.UpdatePromotionAsync(existingPromotion);

        if (!success)
        {
            return StatusCode(500, "A problem happened while handling your request.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePromotion(int id)
    {
        var existingPromotion = await _promotionRepository.GetPromotionByIdAsync(id);
        if (existingPromotion == null)
        {
            return NotFound();
        }

        var success = await _promotionRepository.DeletePromotionAsync(id);

        if (!success)
        {
            return StatusCode(500, "A problem happened while handling your request.");
        }

        return NoContent();
    }
}
