using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace;

public class HealthController  :ControllerBase
{
    [HttpGet("IsReady")]
    public async Task<bool> IsReady()
    {
       return await Task.FromResult(true);
    }
}