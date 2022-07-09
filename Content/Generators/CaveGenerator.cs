using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class CaveGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed, WorldGenConfiguration configuration)
		{
			return new List<GenPass>
			{
				new PassLegacy("Placing Stone", (progress, configuration) =>
				{
					progress.Message = "Placing Stone";
					for (var i = 0; i < Main.maxTilesX; i++)
					for (var j = 0; j < Main.maxTilesY; j++)
					{
						progress.Set((float) i / Main.maxTilesX);
						WorldGen.PlaceTile(i, j, TileID.Stone, true, true);
					}
				}, 80f),
				new PassLegacy("Making Swiss Cheese", (progress, configuration) =>
				{
					progress.Message = "Making Swiss Cheese";
					Main.worldSurface = 0;
					Main.rockLayer = 0;
					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00013); ++index)
					{
						var num38 = index / (Main.maxTilesX * Main.maxTilesY * 0.00013f);
						progress.Set(num38);
						if (WorldGen.rockLayerHigh <= Main.maxTilesY)
						{
							var type = -1;
							if (WorldGen.genRand.NextBool(10))
								type = -2;
							WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
								WorldGen.genRand.Next(type == -2 ? Main.maxTilesY * 3 / 4 : 0, Main.maxTilesY),
								WorldGen.genRand.Next(30, 60), WorldGen.genRand.Next(400, 600), type);
						}
					}
				}, 10f),
				new PassLegacy("Shinies", (progress, _) =>
				{
					progress.Message = "Shinies";
					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.0002); ++index)
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next(0, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), TileID.Copper);
					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.0002); ++index)
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next(0, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), TileID.Iron);
					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00015); ++index)
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next(0, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), TileID.Silver);
					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00015); ++index)
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next(0, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), TileID.Gold);
				}, 5f),
				new PassLegacy("Settling Lava", (progress, configuration) =>
				{
					progress.Message = "Settling Lava";
					Liquid.worldGenTilesIgnoreWater(true);
					Liquid.QuickWater(3);
					WorldGen.WaterCheck();
					var num208 = 0;
					Liquid.quickSettle = true;
					while (num208 < 10)
					{
						var num209 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
						++num208;
						var num210 = 0.0f;
						while (Liquid.numLiquid > 0)
						{
							var num211 = (num209 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) /
							             (float) num209;
							if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num209)
								num209 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
							if (num211 > (double) num210)
								num210 = num211;
							else
								num211 = num210;
							if (num208 == 1)
								progress.Set((float) (num211 / 3.0 + 0.330000013113022));
							var num212 = 10;
							if (num208 > num212)
							{
							}

							Liquid.UpdateLiquid();
						}

						WorldGen.WaterCheck();
						progress.Set((float) (num208 * 0.100000001490116 / 3.0 + 0.660000026226044));
					}

					Liquid.quickSettle = false;
					Liquid.worldGenTilesIgnoreWater(false);
					Main.tileSolid[484] = false;
				}, 5f)
			};
		}
	}
}