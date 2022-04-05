using SubworldLibrary;
using Terraria.ModLoader;

namespace Multiverse2.Content.Commands
{
	public class MultiverseCommand : ModCommand
	{
		public override string Command => "mvtp";

		public override CommandType Type => CommandType.Chat;

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length != 1)
				return;
			if (!args[0].Contains('/'))
				SubworldSystem.Enter("Multiverse2/" + args[0]);
			else
				SubworldSystem.Enter(args[0]);
		}
	}
}