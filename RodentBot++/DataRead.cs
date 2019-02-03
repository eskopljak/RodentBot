using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RodentBot
{
	static class DataRead
	{
		private static readonly string startingPart = "{\"payload\":{\"event_name\":\"MetagameEvent\"";

		private static string GetContinent(int id)
		{
			// continent id's
			// Indar - 2, Hossin - 4, Amerish - 6, Esamir - 8

			if ((id <= 158 && id >= 156) || (id <= 134 && id >= 132)) return "Amerish";

			if ((id <= 155 && id >= 153) || (id <= 131 && id >= 129)) return "Hossin";

			if ((id <= 152 && id >= 150) || (id <= 128 && id >= 126)) return "Esamir";

			if ((id <= 149 && id >= 147) || (id <= 125 && id >= 123)) return "Indar";

			return "";
		}

		private static string GetWorld(int id)
		{
			switch(id)
			{
				case 19:
					return "Jaeger";
				case 25:
					return "Briggs";
				case 10:
					return "Miller";
				case 13:
					return "Cobalt";
				case 1:
					return "Connery";
				case 17:
					return "Emerald";
				default:
					return "";
			}
		}

		public static bool LockAlert(string data, out string continent)
		{
			if (data.StartsWith(startingPart))
			{
				// alert	

				try
				{
					JObject jObject = JObject.Parse(data);

					JObject payload = (JObject)jObject.SelectToken("payload");

					int metagame_event_id = (int)payload.SelectToken("metagame_event_id");
					
					continent = GetContinent(metagame_event_id);

					string metagame_event_state_name = (string)payload.SelectToken("metagame_event_state_name");

					// int world_id = (int)payload.SelectToken("world_id");
					
					if (continent != "" && metagame_event_state_name != "ended") 
					{
						return true;
					}
				}
				catch
				{
				}				
			}

			continent = "";
			return false;
		}
	}
}
