using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;


namespace RodentBot
{
	static class Bot
	{
		private const string _token = ""; //placeholder
		
		private static DiscordClient discord;
		private static CommandsNextModule commands;

		private static HashSet<DiscordChannel> channels = new HashSet<DiscordChannel>(new DiscordChannelComparer());

		public static async Task MainAsync(string[] args)
		{
			discord = new DiscordClient(new DiscordConfiguration
			{
				Token = _token,
				TokenType = TokenType.Bot
			});

			commands = discord.UseCommandsNext(new CommandsNextConfiguration
			{
				StringPrefix = ".",
				EnableDms = false
			});

			commands.RegisterCommands<Commands>();

			try
			{
				await discord.ConnectAsync();
			}
			catch
			{
				//
			}

			await Task.Delay(-1);
		}

		public static async Task Shutdown()
		{
			try
			{
				await discord.DisconnectAsync();
			}
			catch
			{
				//
			}
		}

		public static bool AddChannel(DiscordChannel channel)
		{
			return channels.Add(channel);
		}

		public static bool DeleteChannel(DiscordChannel channel)
		{
			return channels.Remove(channel);
		}

		public static void SendMsgToSubscribedChannels(string message)
		{
			foreach(DiscordChannel ch in channels)
			{
				SendMsgToChannel(ch, message);
			}
		}

		private static async void SendMsgToChannel(DiscordChannel channel, string message)
		{
			try
			{
				await channel.SendMessageAsync(message);
			}
			catch
			{
				//
			}
		}
	}

	class DiscordChannelComparer : IEqualityComparer<DiscordChannel>
	{
		public bool Equals(DiscordChannel x, DiscordChannel y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(DiscordChannel obj)
		{
			return obj.GetHashCode();
		}
	}


}
