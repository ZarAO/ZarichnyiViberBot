using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Viber.Bot;
using ViberBotServer.Viber;

namespace BotServer.Controllers
{
    [Route("[controller]")]
    public class ViberBotController : Controller
    {
        private readonly ILogger<ViberBotController> _logger;
        private readonly ViberBot _viberBot;
        private readonly EventsHandler _eventsHandler;

        public ViberBotController(ILogger<ViberBotController> logger) {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            ViberBotOptions viberBotOptions = config.GetSection("ViberBot").Get<ViberBotOptions>();

            _logger = logger;
            _viberBot = new ViberBot(viberBotOptions);
            _eventsHandler = new EventsHandler(_viberBot);
        }

        [Route("SetWebHook")]
        [HttpGet]
        public async Task<IActionResult> SetWebHook() {
            try {
                await _viberBot.SetWebhookAsync();
                _logger.LogDebug("Task: SetWebHook");
                return Ok();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error: {ex.Message}", ex.Message);

                return BadRequest(ex.Message);
            }
        }
        [Route("Index")]
        [HttpPost]
        public async Task<IActionResult> Index() {
            try {
                _logger.LogDebug("Task: Index");

                string body;
                using (StreamReader reader = new StreamReader(HttpContext.Request.Body)) {
                    body = await reader.ReadToEndAsync();
                }
                var isSignatureValid = _viberBot.viberBotClient.ValidateWebhookHash(
                    HttpContext.Request.Headers[ViberBotClient.XViberContentSignatureHeader],
                    body);



                _logger.LogDebug("CallbackData: " + body);

                if (!isSignatureValid) {
                    throw new Exception("Invalid viber content signature");
                }

                CallbackData callbackData = JsonConvert.DeserializeObject<CallbackData>(body);

                var user = callbackData.User;

                switch (callbackData.Event) {
                    case EventType.ConversationStarted:
                        await _eventsHandler.onConversationStarted(user.Id, user.Name, user.Avatar);
                        break;
                    case EventType.Subscribed:
                        await _eventsHandler.onSubscribed(user.Id);
                        break;
                    case EventType.Unsubscribed:
                        await _eventsHandler.onUnSubscribed(callbackData.UserId);
                        break;
                    case EventType.Message:
                        await _eventsHandler.onReceiveMessage(callbackData.Sender.Id, callbackData.MessageToken, callbackData.Message);
                        break;
                }

                return Ok();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error: {ex.Message}", ex.Message);

                return BadRequest(ex.Message);
            }
        }

    }
}
