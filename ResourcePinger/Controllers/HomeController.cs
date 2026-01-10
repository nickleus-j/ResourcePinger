using Microsoft.AspNetCore.Mvc;
using ResourcePinger.Models;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Pinger.Util;

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
                //bool status = IsWebsiteUp_Ping(q).Result;
				var status= IsWebsiteUp(q).Result;
                pingStatus.IsSuccessful = status.Status == IPStatus.Success;
                if (pingStatus.IsSuccessful)
                {
                    pingStatus.ReturnedAtUtc = DateTime.UtcNow;
					pingStatus.RoundtripTime = DateTime.UtcNow.Ticks;
                }
			}
			catch
			{
				pingStatus.IsSuccessful = false;
                pingStatus.ReturnedAtUtc = DateTime.UtcNow;
				pingStatus.RoundtripTime = 0;
            }
			
			return Json(pingStatus);
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
            return View(IsWebsiteUp_Ping("localhost").Result);
		}
        public IActionResult Qr(string q)
        {
            var svgString = QrUtil.GetQrSvgOfUrl(q,10);
			Dictionary<string, string> result=new Dictionary<string, string>();
			result.Add("svg",svgString);
            result.Add("url", q);
			return Json(result);
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
        private async Task<PingReply> IsWebsiteUp(string url)
        {
            Ping ping = new Ping();
            var hostName = new Uri(url).Host;

            PingReply result = await ping.SendPingAsync(hostName);

			return result;
        }
    }
}
