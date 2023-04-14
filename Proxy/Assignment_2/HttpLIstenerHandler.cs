using System.Net;
using System.Text;

namespace Assignment_2
{
	public class HttpLIstenerHandler
	{
		private readonly string _proxyUrl;
		private readonly string _pageUrl;
		private readonly string _replacement;
		private readonly HttpListener _listener;
		/// <summary>
		/// Initilize new HttpLIstenerHandler
		/// </summary>
		/// <param name="listener">HttpListener</param>
		/// <param name="proxyUrl">Disired url to replase original</param>
		/// <param name="pageUrl">Original url of web page</param>
		/// <param name="replacement">What to add in the end of 6 letter word</param>
		public HttpLIstenerHandler(HttpListener listener, string proxyUrl = "http://localhost:8086/", string pageUrl = "https://www.reddit.com", string replacement = "$1™")
		{
			_proxyUrl = proxyUrl;
			_pageUrl = pageUrl;
			_replacement = replacement;
			_listener = listener;
			_listener.Prefixes.Add(proxyUrl);

		}

		/// <summary>
		/// Start handling Requests
		/// </summary>
		public async Task HandleStart()
		{
			_listener.Start();
			while (true)
			{
				HttpListenerContext context = _listener.GetContext();
				await HandleRequest(context);
			}
		}

		/// <summary>
		/// Handling request and applying desired replacement
		/// </summary>
		/// <param name="context">Http Listener Context</param>
		/// <exception cref="ArgumentNullException"></exception>
		private async Task HandleRequest(HttpListenerContext context)
		{
			var url = context.Request?.Url?.AbsoluteUri.Replace(_proxyUrl, _pageUrl);
			if (url is null) throw new ArgumentNullException($"{nameof(url)} can not be null");

			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(url);

				string responseBody = await response.Content.ReadAsStringAsync();
				responseBody = responseBody.ModifyResponseBody(_proxyUrl, _replacement);

				byte[] buffer = Encoding.UTF8.GetBytes(responseBody);
				context.Response.ContentLength64 = buffer.Length;
				Stream output = context.Response.OutputStream;
				output.Write(buffer, 0, buffer.Length);
			}
		}


	}
}
