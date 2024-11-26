using market_api.Controllers;
using market_api.models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DataBaseContext _context;

    public AuthController(DataBaseContext context)
    {
        _context = context;
    }


    [HttpPost("register")]
    public IActionResult Register([FromBody] userRequest request)
    {
        // Check if the username already exists
        var existingUser = _context.Users.SingleOrDefault(u => u.Username == request.Username);
        if (existingUser != null)
        {
            return BadRequest("Username already taken");
        }

        // Hash the password
        string hashedPassword = PasswordHelper.HashPassword(request.Password);

        // Create a new user
        var user = new User
        {
            Username = request.Username,
            PasswordHash = hashedPassword
        };

        // Save the user in the database
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] userRequest request)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);

        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid username or password");
        }

        // Optionally return JWT token
        return Ok("Login successful");
    }
}
