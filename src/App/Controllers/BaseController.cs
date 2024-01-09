using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
}