using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Microsoft.Xna.Framework;
using Multiverse2.Content.Configs;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class VanillaGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed) => new()
		{
			new PassLegacy("Vanilla", (progress, _) =>
			{
				var currentGenerationProgress = WorldGenerator.CurrentGenerationProgress;
				WorldGen.GenerateWorld(seed, progress);
				WorldGenerator.CurrentGenerationProgress = currentGenerationProgress;
			})
		};
	}
}