using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Navigator.Abstractions;

namespace Navigator.Extensions.Shipyard.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("shipyard/api/v{v:apiVersion}/[controller]")]
    public class BotController : ControllerBase
    {
        [HttpGet("information")]
        public async Task<IActionResult> GetBotInformation([FromServices] IBotClient _botClient)
        {
            var information = await _botClient.GetMeAsync();
            
            return Ok(information);
        }

        [HttpGet("botpic")]
        public async Task<IActionResult> GetBotPic([FromServices] IBotClient botClient)
        {
            
            var photos = await botClient.GetUserProfilePhotosAsync(botClient.BotId);

            if (photos.TotalCount > 0)
            {
                var botpicId = photos.Photos.FirstOrDefault()?
                    .OrderByDescending(x => x.FileSize).FirstOrDefault()
                    ?.FileId;

                if (!string.IsNullOrWhiteSpace(botpicId))
                {
                    var botpic = new MemoryStream();
                    await botClient.GetInfoAndDownloadFileAsync(botpicId, botpic);
                
                    return File(botpic.ToArray(), "image/png");   
                }
            }

            return NotFound("BotPic not found");
        }
    }
}