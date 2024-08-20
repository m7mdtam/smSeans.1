using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.password))
        {
            return BadRequest("Password is required.");
        }

        var user = new User
        {
            username = userDto.username,
            email = userDto.email,
            password = userDto.password,
            role_name = userDto.role_name ?? "User",
            google_id= userDto.google_id,
            status = userDto.status,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow,
            phone_number = userDto.phone_number,
            first_name=userDto.first_name,
            last_name=userDto.last_name,
            
            

            
        };

        var userId = await _userRepository.CreateUserAsync(user);

        return Ok(userId);


    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
    {
        if (userDto == null || id <= 0)
        {
            return BadRequest("Invalid user data.");
        }

        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.username = userDto.username;
        user.email = userDto.email;

        await _userRepository.UpdateUserAsync(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userRepository.DeleteUserAsync(id);

        return NoContent();
    }
}
