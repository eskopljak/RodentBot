using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace RodentBot
{
	class Program
	{
		static void Main(string[] args)
		{
			MessageHandling.Init(new AlertDiscordHandling());

			Bot.MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
		}
	}
}
