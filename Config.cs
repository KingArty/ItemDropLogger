using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace ItemDropLog
{
	[JsonObject]
	public sealed class Config
	{
		private static Config instance;

		public static Config Instance
		{
			get
			{
				return Config.instance;
			}
		}

		[JsonProperty("ignoredItems")]
		public IList<string> IgnoredItems
		{
			get;
			set;
		}

		public Config()
		{
			this.IgnoredItems = new List<string>{};
		}

		public static void CreateInstance(string path)
		{
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Config.instance = new Config();
                return;
            }
            else
            {
                using (Stream stream = File.OpenRead(path))
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        Config.instance = JsonConvert.DeserializeObject<Config>(streamReader.ReadToEnd());
                        return;
                    }
                }
            }
		}

		public static void SaveInstance(string path)
		{
			if (Config.instance == null)
			{
				return;
			}
			using (Stream stream = File.Create(path))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					string value = JsonConvert.SerializeObject(Config.instance, Formatting.Indented);
					streamWriter.WriteLine(value);
				}
			}
		}
	}
}
