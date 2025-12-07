using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers;

public class HealthController  :ControllerBase
{
    [HttpGet("IsReady")]
    public async Task<bool> IsReady()
    {
       return await Task.FromResult(true);
    }
}