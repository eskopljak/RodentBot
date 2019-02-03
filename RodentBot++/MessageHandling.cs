using System;
using System.Collections.Generic;
using System.Text;

namespace RodentBot
{
	static class MessageHandling
	{
		private static WebSocketConnection ws = null;

		static IOnAlertStart OnAlertStart = null;

		public static void HandleMsg(string message)
		{
			if (!message.StartsWith("{\"o"))
			{
				if (DataRead.LockAlert(message, out string continent))
				{
					OnAlertStart.ProcessAlert(continent);
				}
			}			
		}
	
		public static void Init(IOnAlertStart onAlertStart)
		{
			OnAlertStart = onAlertStart;

			if(ws == null) ws = new WebSocketConnection();
		}
	}
}
