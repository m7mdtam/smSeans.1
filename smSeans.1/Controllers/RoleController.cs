using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);
        if (role == null)
            return NotFound();

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto roleDto)
    {
        if (roleDto == null)
        {
            return BadRequest("Role data is required.");
        }

        var role = new Role
        {
            role_name = roleDto.role_name,
            description = roleDto.description
        };

        var createdRoleId = await _roleRepository.CreateRoleAsync(role);

        return CreatedAtAction(nameof(GetRoleById), new { id = createdRoleId }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleUpdateDto roleDto)
    {
        if (roleDto == null || id <= 0)
        {
            return BadRequest("Invalid role data.");
        }

        var existingRole = await _roleRepository.GetRoleByIdAsync(id);
        if (existingRole == null)
        {
            return NotFound();
        }

        existingRole.role_name = roleDto.role_name;
        existingRole.description = roleDto.description;

        var isUpdated = await _roleRepository.UpdateRoleAsync(existingRole);
        if (!isUpdated)
        {
            return StatusCode(500, "An error occurred while updating the role.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var existingRole = await _roleRepository.GetRoleByIdAsync(id);
        if (existingRole == null)
        {
            return NotFound();
        }

        var isDeleted = await _roleRepository.DeleteRoleAsync(id);
        if (!isDeleted)
        {
            return StatusCode(500, "An error occurred while deleting the role.");
        }

        return NoContent();
    }
}
