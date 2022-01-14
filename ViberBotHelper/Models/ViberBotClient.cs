using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net;

namespace Viber.Bot
{
	/// <summary>
	/// Viber API.
	/// </summary>
	public class ViberBotClient : IViberBotClient, IDisposable
	{
		public const string XViberContentSignatureHeader = "X-Viber-Content-Signature";

		private const string XViberAuthTokenHeader = "X-Viber-Auth-Token";

		private const string BaseAddress = "https://chatapi.viber.com/pa/";

		private readonly HttpClient _httpClient;

		private readonly HMACSHA256 _hashAlgorithm;

		private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

		public ViberBotClient(string authenticationToken) : this(authenticationToken, null)
		{}

		public ViberBotClient(string authenticationToken, IWebProxy proxy)
		{
			_httpClient = proxy == null
				? new HttpClient()
				: new HttpClient(new HttpClientHandler { Proxy = proxy, UseProxy = true });
			_httpClient.BaseAddress = new Uri(BaseAddress);
			_httpClient.DefaultRequestHeaders.Add(XViberAuthTokenHeader, new[] { authenticationToken });

			_hashAlgorithm = new HMACSHA256(Encoding.UTF8.GetBytes(authenticationToken));
		}

		public async Task<ICollection<EventType>> SetWebhookAsync(string url, ICollection<EventType> eventTypes = null)
		{
			var result = await RequestApiAsync<SetWebhookResponse>(
				"set_webhook",
				new Dictionary<string, object>
				{
					{ "url", url },
					{ "event_types", eventTypes }
				});
			return result.EventTypes;
		}

		public async Task<IAccountInfo> GetAccountInfoAsync()
		{
			return await RequestApiAsync<GetAccountInfoResponse>("get_account_info");
		}

		public async Task<UserDetails> GetUserDetailsAsync(string id)
		{
			var response = await RequestApiAsync<GetUserDetailsResponse>(
				"get_user_details",
				new Dictionary<string, object> { { "id", id } });
			return response.User;
		}

		public Task<long> SendTextMessageAsync(TextMessage message) => SendMessageAsync(message);

		public Task<long> SendPictureMessageAsync(PictureMessage message) => SendMessageAsync(message);

		public Task<long> SendFileMessageAsync(FileMessage message) => SendMessageAsync(message);

		public Task<long> SendVideoMessageAsync(VideoMessage message) => SendMessageAsync(message);

		public Task<long> SendContactMessageAsync(ContactMessage message) => SendMessageAsync(message);

		public Task<long> SendLocationMessageAsync(LocationMessage message) => SendMessageAsync(message);

		public Task<long> SendUrlMessageAsync(UrlMessage message) => SendMessageAsync(message);

		public Task<long> SendStickerMessageAsync(StickerMessage message) => SendMessageAsync(message);

		public Task<long> SendKeyboardMessageAsync(KeyboardMessage message) => SendMessageAsync(message);

		public Task<long> SendRichMediaMessageAsync(RichMediaMessage message) => SendMessageAsync(message);

		public Task<long> SendBroadcastMessageAsync(BroadcastMessage message) => SendMessageAsync(message, true);

		public bool ValidateWebhookHash(string signatureHeader, string jsonMessage)
		{
			if (signatureHeader == null || jsonMessage == null)
			{
				return false;
			}

			try
			{
				var hash = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(jsonMessage));

			#if NET461
				var signature = System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Parse(signatureHeader).Value;
			#else
				var signature = ParseHex(signatureHeader);
			#endif

				return StructuralComparisons.StructuralEqualityComparer.Equals(hash, signature);
			}
			catch
			{
				return false;
			}
		}
		private async Task<long> SendMessageAsync(MessageBase message, bool isBroadcast = false)
		{
			var result = await RequestApiAsync<SendMessageResponse>(isBroadcast ? "broadcast_message" : "send_message", message);
			return result.MessageToken;
		}
		private async Task<T> RequestApiAsync<T>(string method, object data = null)
			where T : ApiResponseBase, new()
		{
			var requestJson = data == null
				? "{}"
				: JsonConvert.SerializeObject(data, _jsonSettings);
			var response = await _httpClient.PostAsync(method, new StringContent(requestJson));
			var responseJson = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<T>(responseJson);
			if (result.Status != ErrorCode.Ok)
			{
				throw new ViberRequestApiException(
					result.Status,
					result.StatusMessage,
					method,
					requestJson,
					responseJson);
			}

			return result;
		}

		#if !NET461
		private byte[] ParseHex(string hex)
		{
			var numberChars = hex.Length;
			var bytes = new byte[numberChars / 2];
			for (var i = 0; i < numberChars; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}

			return bytes;
		}
		#endif
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_httpClient.Dispose();
				_hashAlgorithm.Dispose();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
