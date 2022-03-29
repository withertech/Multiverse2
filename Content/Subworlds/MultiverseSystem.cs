using Multiverse2.Content.Configs;
using Multiverse2.Content.Generators;
using SubworldLibrary;
using Terraria.ModLoader;

namespace Multiverse2.Content.Subworlds
{
	public class MultiverseSystem : ModSystem
	{
		public override void OnModLoad()
		{
			foreach (var world in ModContent.GetInstance<MultiverseConfig>().Worlds)
			{
				Mod.AddContent(new MultiverseWorld(world));
			}
		}
	}
}