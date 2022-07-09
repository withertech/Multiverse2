using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class JungleGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed, WorldGenConfiguration configuration)
		{
			return new List<GenPass>
			{
				new PassLegacy("Placing Mud", (progress, _) =>
				{
					progress.Message = "Placing Mud";
					for (var i = 0; i < Main.maxTilesX; i++)
					for (var j = 0; j < Main.maxTilesY; j++)
					{
						progress.Set((float) i / Main.maxTilesX);
						WorldGen.PlaceTile(i, j, TileID.Mud, true, true);
					}
				}, 80f),
				new PassLegacy("Making Swiss Cheese", (progress, _) =>
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
								WorldGen.genRand.Next(0, Main.maxTilesY),
								WorldGen.genRand.Next(30, 60), WorldGen.genRand.Next(400, 600), type);
						}
					}

					for (var i = 0; i < Main.maxTilesX; i++)
					for (var j = 0; j < Main.maxTilesY; j++)
					{
						var tile = Main.tile[i, j];
						if (tile.LiquidType != LiquidID.Lava)
							continue;
						if (j < Main.maxTilesY * 3 / 4)
							tile.LiquidType = LiquidID.Water;
					}
				}, 5f),
				new PassLegacy("Spreading Grass", (progress, _) =>
				{
					progress.Message = "Spreading Grass";
					for (var i = 0; i < Main.maxTilesX; i++)
					{
						var percent = (float) i / Main.maxTilesX;
						progress.Set(percent);
						for (var j = 0; j <= Main.maxTilesY; j++)
							WorldGen.SpreadGrass(i, j, TileID.Mud, TileID.JungleGrass);
					}
				}, 5f),
				new PassLegacy("Growing Plants", (progress, _) =>
				{
					progress.Message = "Growing Plants";
					progress.Set(1f);
					for (var index = 0; index < Main.maxTilesX; ++index)
					for (var y = 0; y < Main.maxTilesY; ++y)
					{
						var tile = Main.tile[index, y];
						if (tile.HasTile)
						{
							if (y >= (int) Main.worldSurface)
							{
								tile = Main.tile[index, y];
								if (tile.TileType == 70)
								{
									tile = Main.tile[index, y - 1];
									if (!tile.HasTile)
									{
										WorldGen.GrowTree(index, y);
										tile = Main.tile[index, y - 1];
										if (!tile.HasTile)
										{
											WorldGen.GrowTree(index, y);
											tile = Main.tile[index, y - 1];
											if (!tile.HasTile)
											{
												WorldGen.GrowShroom(index, y);
												tile = Main.tile[index, y - 1];
												if (!tile.HasTile)
													WorldGen.PlaceTile(index, y - 1, 71, true);
											}
										}
									}
								}
							}

							tile = Main.tile[index, y];
							if (tile.TileType == 60)
							{
								tile = Main.tile[index, y - 1];
								if (!tile.HasTile)
									WorldGen.PlaceTile(index, y - 1, 61, true);
							}
						}
					}

					for (var index = 0; index < Main.maxTilesX * 100; ++index)
					{
						var num379 = WorldGen.genRand.Next(40, Main.maxTilesX / 2 - 40);
						var y = WorldGen.genRand.Next(Main.maxTilesY - 300);
						Tile tile;
						while (true)
						{
							tile = Main.tile[num379, y];
							if (!tile.HasTile && y < Main.maxTilesY - 300)
								++y;
							else
								break;
						}

						tile = Main.tile[num379, y];
						if (tile.HasTile)
						{
							tile = Main.tile[num379, y];
							if (tile.TileType == 60)
							{
								var num380 = y - 1;
								WorldGen.PlaceJunglePlant(num379, num380, 233, WorldGen.genRand.Next(8), 0);
								tile = Main.tile[num379, num380];
								if (tile.TileType != 233)
									WorldGen.PlaceJunglePlant(num379, num380, 233, WorldGen.genRand.Next(12), 1);
							}
						}
					}
				}, 5f),
				new PassLegacy("Settling Liquids", (progress, _) =>
				{
					progress.Message = "Settling Liquids";
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