using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RemoteController : Controller
{
    [HttpGet]
    public async Task<ActionResult<string>> Retrieve()
    {
        using var client = new HttpClient();
        var str = await client.GetStringAsync("http://secondary:8080/api/redis/leftpop");
        return Ok(str);
    }

    [HttpGet]
    public async Task<ActionResult<string>> RetrieveError()
    {
        using var client = new HttpClient();
        var str = await client.GetStringAsync("http://secondary:8080/error");
        return Ok(str);
    }
}
