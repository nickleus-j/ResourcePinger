using System.Net.NetworkInformation;

namespace ResourcePinger.Models
{
	public class PingStatus
	{
		public bool IsSuccessful {  get; set; }
		public DateTime? ReturnedAtUtc { get;set; }
        public long RoundtripTime { get; set; }
    }
}
