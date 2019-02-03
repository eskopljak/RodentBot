using System;
using System.Collections.Generic;
using System.Text;

namespace RodentBot
{
	interface IOnAlertStart
	{
		void ProcessAlert(string continent);
	}
}
