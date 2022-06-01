using System.Collections.Generic;
using SubworldLibrary;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class VanillaGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed, WorldGenConfiguration configuration)
		{
			return new List<GenPass>
			{
				new PassLegacy("Vanilla", (progress, _) =>
				{
					var currentGenerationProgress = WorldGenerator.CurrentGenerationProgress;
					SubworldSystem.hideUnderworld = true;
					WorldGen.GenerateWorld(seed, progress);
					WorldGenerator.CurrentGenerationProgress = currentGenerationProgress;
				})
			};
		}
	}
}