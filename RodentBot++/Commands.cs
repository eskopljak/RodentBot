using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace RodentBot
{
	class Commands
	{
		[Command("subscribe")]
		public async Task Subscribe(CommandContext ctx)
		{
			if(Bot.AddChannel(ctx.Channel))
			{
				await ctx.RespondAsync("👌");
			}
		}

		[Command("unsubscribe")]
		public async Task Unsubscribe(CommandContext ctx)
		{
			if (Bot.DeleteChannel(ctx.Channel))
			{
				await ctx.RespondAsync(":(");
			}
		}

		[Command("shutdown")]
		public async Task Shutdown(CommandContext ctx)
		{
			await Bot.Shutdown();
		}

		[Command("info")]
		public async Task Info(CommandContext ctx)
		{
			await ctx.RespondAsync("StompyMaggot, 2018");
		}
	}
}
