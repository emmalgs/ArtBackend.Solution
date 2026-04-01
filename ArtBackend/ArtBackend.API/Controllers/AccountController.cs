using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("account")]
public class AccountController : Controller
{
    private readonly IConfiguration _config;

    public AccountController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("login")]
    public IActionResult Login() => View();

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var adminEmail = _config["Admin:Email"];
        var adminPassword = _config["Admin:Password"];

        if (email != adminEmail || password != adminPassword)
        {
            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
        var identity = new ClaimsIdentity(claims, "AdminCookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("AdminCookie", principal);
        return Redirect("/admin");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("AdminCookie");
        return Redirect("/account/login");
    }
}
