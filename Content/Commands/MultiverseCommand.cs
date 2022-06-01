using Multiverse2.Content.Configs;
using SubworldLibrary;
using Terraria.ModLoader;

namespace Multiverse2.Content.Commands;

public class MultiverseCommand : ModCommand
{
	public override string Command => "mvtp";

	public override CommandType Type => CommandType.Chat;

	public override void Action(CommandCaller caller, string input, string[] args)
	{
		if (args.Length != 1)
			return;


		if (!args[0].Contains('/'))
		{
			if (ModContent.GetInstance<MultiverseConfig>() != null && 
			    ModContent.GetInstance<MultiverseConfig>().TpFilter.Exists(configuration => configuration.Subworld.Mod == "Multiverse2" && configuration.Subworld.Name == args[0] && configuration.Command) ||
			    !ModContent.GetInstance<MultiverseConfig>().TpFilter.Exists(configuration => configuration.Subworld.Mod == "Multiverse2" && configuration.Subworld.Name == args[0]))
			{
				SubworldSystem.Enter("Multiverse2/" + args[0]);
			}
		}
		else
		{
			if (ModContent.GetInstance<MultiverseConfig>() != null && 
			    ModContent.GetInstance<MultiverseConfig>().TpFilter.Exists(configuration => configuration.Subworld.Mod == args[0].Split("/")[0] && configuration.Subworld.Name == args[0].Split("/")[1] && configuration.Command) ||
			    !ModContent.GetInstance<MultiverseConfig>().TpFilter.Exists(configuration => configuration.Subworld.Mod == args[0].Split("/")[0] && configuration.Subworld.Name == args[0].Split("/")[1]))
			{
				SubworldSystem.Enter(args[0]);
			}
		}
	}
}