using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using System.Timers;

namespace RodentBot
{
	class WebSocketConnection
	{
		private readonly string link = "wss://push.planetside2.com/streaming?environment=ps2&service-id=s:Stomp";

		// private readonly string initialMsg = "{\"service\":\"event\",\"action\":\"subscribe\",\"worlds\":[\"10\"],\"eventNames\":[\"PlayerLogin\"]}";

		// private readonly string initialMsg2 = "{\"service\":\"event\",\"action\":\"subscribe\",\"characters\":[\"all\"],\"worlds\":[\"all\"],\"eventNames\":[\"all\"]}";

		private readonly string initialMsgOnlyAlerts = "{\"service\":\"event\",\"action\":\"subscribe\",\"worlds\":[\"10\"],\"eventNames\":[\"MetagameEvent\"]}";

		// string initialMsgTEST = "{\"service\":\"event\",\"action\":\"subscribe\",\"worlds\":[\"all\"],\"eventNames\":[\"MetagameEvent\"]}";

		private WebSocket ws = null;

		private Timer timer;
		private readonly double timerInterval_ms = 45000;
		private bool messageReceived = true;

		public WebSocketConnection()
		{
			Connect();
		
			SetTimer();
		}

		private void Connect()
		{
			Close();
			
			ws = new WebSocket(link);

			ws.OnMessage += (sender, e) =>
			{
				messageReceived = true;

				MessageHandling.HandleMsg(e.Data);
			};

			ws.Connect();

			try
			{
				ws.Send(initialMsgOnlyAlerts);
			}
			catch
			{

			}
		}

		~WebSocketConnection()
		{
			Close();
		}

		private void Close()
		{
			if (ws != null)
			{
				try
				{
					ws.Close();
				}
				catch
				{

				}
			}
		}

		private void SetTimer()
		{
			timer = new Timer(timerInterval_ms);

			timer.Elapsed += (object source, ElapsedEventArgs e) =>
			{
				if(messageReceived == false)
				{
					Connect();
				}

				messageReceived = false;
			};

			timer.AutoReset = true;
			timer.Enabled = true;
		}
	}
}

// without slashes :

// initialMsg1
// {"service":"event","action":"subscribe","worlds":["1"],"eventNames":["PlayerLogin"]}

// initialMSg2
// {"service":"event","action":"subscribe","characters":["all"],"worlds":["all"],"eventNames":["all"]}

//initialMsgOnlyAlerts
// {"service":"event","action":"subscribe","worlds":["10"],"eventNames":["MetagameEvent"]}

//initialMsgTEST
// {"service":"event","action":"subscribe","worlds":["10"],"eventNames":["ContinentLock", "ContinentUnlock", "MetagameEvent"]}


