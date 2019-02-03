using System;
using System.Collections.Generic;
using System.Text;

namespace RodentBot
{
	class AlertDiscordHandling : IOnAlertStart
	{
		public void ProcessAlert(string continent)
		{
			Bot.SendMsgToSubscribedChannels($"@here {continent} Locking !");
		}
	}
}
