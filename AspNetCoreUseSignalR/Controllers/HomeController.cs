using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreUseSignalR.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> SendAll([FromQuery]string message)
        {
            await _hubContext.Clients.All.SendAsync("receiveMessage", message);

            return Content("ok");
        }
    }
}
