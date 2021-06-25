using System;
using System.IO;
using Newtonsoft.Json;

namespace P2PChat.Client.Services
{
	public class JsonConfigParserService : IConfigParserService
	{
		private struct Config
		{
			public Config(string ip, string hostname)
			{
				this.ip = ip;
				this.hostname = hostname;
			}

			public readonly string ip;
			public readonly string hostname;
		}

		public string Ip { get; protected set; }
		public string Host { get; protected set; }
		private string filePath;

		public JsonConfigParserService(string filePath)
		{
			this.filePath = filePath;
		}

		public void Parse()
		{
			using (var file = new StreamReader(filePath))
			{
				string text = file.ReadToEnd();
				if (text == "")
					throw new Exception("Config file is empty");

				try
				{
					var data = JsonConvert.DeserializeObject<Config>(text);
					string host = data.hostname;
					string ip = data.ip;
					if (!string.IsNullOrEmpty(host))
						Host = host;

					if (!string.IsNullOrEmpty(ip))
						Ip = ip;
				}
				catch (JsonException ex)
				{
					throw new Exception("Config file has wrong format");
				}
			}
		}
	}
}