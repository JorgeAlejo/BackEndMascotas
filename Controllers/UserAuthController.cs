//TODO: Set with the DB

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserAuthController : ControllerBase{
    private readonly AuthService _authService;

    public UserAuthController(AuthService authService){
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await _authService.Register(user)) return Conflict("A user with the same email already exists.");
        return Ok(new { Message = "User registered successfully." });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user){
        var loggedInUser = _authService.Login(user.Email, user.Password);
        if (loggedInUser == null) return Unauthorized("Invalid email or password.");
        return Ok(new { Message = "Login Successful.", UserId = loggedInUser.Id });
    }
}