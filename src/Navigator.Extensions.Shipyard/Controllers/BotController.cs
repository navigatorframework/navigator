using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Navigator.Extensions.Shipyard.Abstractions;
using Navigator.Extensions.Shipyard.Middleware;

namespace Navigator.Extensions.Shipyard.Controllers
{
    /// <summary>
    /// Bot controller.
    /// </summary>
    [Authorize(AuthenticationSchemes = nameof(ShipyardApiKeyAuthenticationHandler))]
    [ApiController]
    [ApiVersion("1")]
    [Route("shipyard/api/v{v:apiVersion}/[controller]")]
    public class BotController : ControllerBase
    {
        private readonly IBotManagementService _botManagementService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="botManagementService"></param>
        public BotController(IBotManagementService botManagementService)
        {
            _botManagementService = botManagementService;
        }

        [HttpGet("information")]
        public async Task<IActionResult> GetBotInformation(CancellationToken cancellationToken)
        {
            var information = await _botManagementService.GetBotInfo(cancellationToken);
            
            return Ok(information);
        }

        [HttpGet("pic")]
        public async Task<IActionResult> GetBotPic(CancellationToken cancellationToken)
        {
            var botPic = await _botManagementService.GetBotPic(cancellationToken);

            if (botPic is not null)
            {
                return File(botPic.File, botPic.MimeType);
            }
            return NotFound("BotPic not found");
        }

        [HttpGet("commands")]
        public async Task<IActionResult> GetBotCommands(CancellationToken cancellationToken)
        {
            var botCommands = await _botManagementService.GetBotCommands(cancellationToken);

            return Ok(botCommands);
        }

        [HttpPut("commands")]
        public async Task<IActionResult> UpdateBotCommands()
        {
            throw new NotImplementedException();
        }

    }
}