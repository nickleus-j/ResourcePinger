using Microsoft.AspNetCore.Mvc;
using ResourcePinger.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ResourcePinger.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		public IActionResult Check(string q)
		{
			if (String.IsNullOrEmpty(q))
			{
				return Error();
			}
			PingStatus pingStatus = new PingStatus();
			try
			{
                bool status = IsWebsiteUp_Ping(q).Result;
                pingStatus.IsSuccessful = status;
                if (status)
                {
                    pingStatus.ReturnedAtUtc = DateTime.UtcNow;
                }
			}
			catch
			{
				pingStatus.IsSuccessful = false;
                pingStatus.ReturnedAtUtc = DateTime.UtcNow;
            }
			
			return Json(pingStatus);
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		private async Task<bool> IsWebsiteUp_Ping(string url)
		{
			Ping ping = new Ping();
			var hostName = new Uri(url).Host;

			PingReply result = await ping.SendPingAsync(hostName);
			return result.Status == IPStatus.Success;
		}
	}
}
