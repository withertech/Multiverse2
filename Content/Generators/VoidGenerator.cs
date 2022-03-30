using System.Collections.Generic;
using Multiverse2.Content.Configs;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class VoidGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed) => new()
		{
			new PassLegacy("Spawn Point", (_, _) =>
			{
				Main.spawnTileX = Main.maxTilesX / 2;
				Main.spawnTileY = Main.maxTilesY / 2;
			}),
			new PassLegacy("Spawn Island", (progress, _) =>
			{
				progress.Message = "Spawn Island";
				Main.worldSurface = Main.maxTilesY - 42;
				Main.rockLayer = Main.maxTilesY - 64;
				WorldGen.CloudIsland(Main.spawnTileX, Main.spawnTileY);
			}),
			new PassLegacy("Spreading Grass", (progress, _) =>
			{
				progress.Message = "Spreading Grass";
				for (var i = 0; i < Main.maxTilesX; i++)
				{
					var percent = (float) i / Main.maxTilesX;
					progress.Set(percent);
					for (var j = Main.spawnTileY - 5; j <= Main.spawnTileY + 5; j++)
					{
						WorldGen.SpreadGrass(i, j);
					}
				}
			}),
			new PassLegacy("Growing Trees", (progress, _) =>
			{
				progress.Message = "Growing Trees";
				WorldGen.AddTrees();
				progress.Set(100.0f);
			}),
			new PassLegacy("Growing Plants", (progress, _) =>
			{
				progress.Message = "Growing Plants";
				WorldGen.PlantAlch();
				progress.Set(100.0f);
			})
		};
	}
}