using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Viber.Bot.NetCore.Infrastructure;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;
using Newtonsoft.Json.Linq;

namespace ZarichnyiViberBot.Controllers
{
    [Route("~/api/")]
    [ApiController]
    public class ViberController : ControllerBase
    {
        private readonly IViberBotApi _viberBotApi;
        private readonly IConfiguration _configuration;

        public ViberController(IViberBotApi viberBotApi, IConfiguration configuration)
        {
            _viberBotApi = viberBotApi;
            _configuration = configuration;
        }
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _viberBotApi.SetWebHookAsync(new ViberWebHook.WebHookRequest(_configuration.GetValue<string>("ViberBot:Webhook")));

            if (response.Content.Status == ViberErrorCode.Ok)
            {
                return Ok("Viber-bot is active");
            }
            else
            {
                return BadRequest(response.Content.StatusMessage);
            }
        }
        [Route("post")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object update)
        {
            var rusult = Ok();

            /*switch (update.Event)
            {
                case ViberEventType.Webhook:
                    {
                        rusult = Ok();

                        break;
                    }

                default: break;
            }

            // you should to control required fields
            var message = new ViberMessage.TextMessage()
            {
                //required
                Receiver = update.Sender.Id,
                Sender = new ViberUser.User()
                {
                    //required
                    Name = "Our bot",
                    Avatar = "http://dl-media.viber.com/1/share/2/long/bots/generic-avatar%402x.png"
                },
                //required
                Text = str
            };

            // our bot returns incoming text
            var response = await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);*/

            return rusult;
        }
    }
}
