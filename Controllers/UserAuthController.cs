using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserAuthController : ControllerBase{
    private readonly AuthService _authService;

    public UserAuthController(AuthService authService){
        _authService = authService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(){
        var users = await _authService.GetAllUsers();
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user){
        var result = await _authService.Register(user);
        if(!result) return BadRequest("El correo ya esta en uso.");
        return Ok("Registro exitoso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user){
        var login = await _authService.Login(user.Email, user.Password);
        if (login == null) return Unauthorized("Correo o contraseña incorrectos.");
        return Ok(login.Id);
    }
}