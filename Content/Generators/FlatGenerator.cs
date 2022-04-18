using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class FlatGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed, WorldGenConfiguration configuration)
		{
			return new List<GenPass>
			{
				new PassLegacy("Placing Dirt", (progress, _) =>
				{
					progress.Message = "Placing Dirt";
					Main.worldSurface = Main.maxTilesY - 42;
					Main.rockLayer = Main.maxTilesY - 64;
					for (var i = 0; i < Main.maxTilesX; i++)
					{
						var percent = (float) i / Main.maxTilesX;
						progress.Set(percent);
						for (var ii = Main.spawnTileY + 30; ii > Main.spawnTileY; ii--)
							WorldGen.PlaceTile(i, ii, TileID.Dirt, true, true);
					}
				}, 20f),
				new PassLegacy("Placing Stone", (progress, _) =>
				{
					progress.Message = "Placing Stone";
					for (var i = 0; i < Main.maxTilesX; i++)
					{
						var percent = (float) i / Main.maxTilesX;
						progress.Set(percent);
						for (var ii = Main.maxTilesY; ii > Main.spawnTileY + 30; ii--)
							WorldGen.PlaceTile(i, ii, TileID.Stone, true, true);
					}
				}, 60f),
				new PassLegacy("Shinies", (progress, _) =>
				{
					progress.Message = "Shinies";
					var copper = 7;
					var iron = 6;
					var silver = 9;
					var gold = 8;
					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 6E-05); ++index)
					{
						if (WorldGen.drunkWorldGen)
							copper = WorldGen.genRand.Next(2) != 0 ? 166 : 7;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.worldSurfaceLow, (int) WorldGen.worldSurfaceHigh),
							WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), copper);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 8E-05); ++index)
					{
						if (WorldGen.drunkWorldGen)
							copper = WorldGen.genRand.Next(2) != 0 ? 166 : 7;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.worldSurfaceHigh, (int) WorldGen.rockLayerHigh),
							WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 7), copper);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.0002); ++index)
					{
						if (WorldGen.drunkWorldGen)
							copper = WorldGen.genRand.Next(2) != 0 ? 166 : 7;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.rockLayerLow, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), copper);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 3E-05); ++index)
					{
						if (WorldGen.drunkWorldGen)
							iron = WorldGen.genRand.Next(2) != 0 ? 167 : 6;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.worldSurfaceLow, (int) WorldGen.worldSurfaceHigh),
							WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(2, 5), iron);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 8E-05); ++index)
					{
						if (WorldGen.drunkWorldGen)
							iron = WorldGen.genRand.Next(2) != 0 ? 167 : 6;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.worldSurfaceHigh, (int) WorldGen.rockLayerHigh),
							WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), iron);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.0002); ++index)
					{
						if (WorldGen.drunkWorldGen)
							iron = WorldGen.genRand.Next(2) != 0 ? 167 : 6;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.rockLayerLow, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), iron);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 2.6E-05); ++index)
					{
						if (WorldGen.drunkWorldGen)
							silver = WorldGen.genRand.Next(2) != 0 ? 168 : 9;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.worldSurfaceHigh, (int) WorldGen.rockLayerHigh),
							WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), silver);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00015); ++index)
					{
						if (WorldGen.drunkWorldGen)
							silver = WorldGen.genRand.Next(2) != 0 ? 168 : 9;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.rockLayerLow, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), silver);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00017); ++index)
					{
						if (WorldGen.drunkWorldGen)
							silver = WorldGen.genRand.Next(2) != 0 ? 168 : 9;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next(0, (int) WorldGen.worldSurfaceLow), WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), silver);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 2.6E-05); ++index)
					{
						if (WorldGen.drunkWorldGen)
							gold = WorldGen.genRand.Next(2) != 0 ? 168 : 9;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.worldSurfaceHigh, (int) WorldGen.rockLayerHigh),
							WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), gold);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00015); ++index)
					{
						if (WorldGen.drunkWorldGen)
							gold = WorldGen.genRand.Next(2) != 0 ? 168 : 9;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next((int) WorldGen.rockLayerLow, Main.maxTilesY),
							WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), gold);
					}

					for (var index = 0; index < (int) (Main.maxTilesX * Main.maxTilesY * 0.00017); ++index)
					{
						if (WorldGen.drunkWorldGen)
							gold = WorldGen.genRand.Next(2) != 0 ? 168 : 9;
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
							WorldGen.genRand.Next(0, (int) WorldGen.worldSurfaceLow), WorldGen.genRand.Next(4, 9),
							WorldGen.genRand.Next(4, 8), gold);
					}

				}, 10f),
				new PassLegacy("Spreading Grass", (progress, _) =>
				{
					progress.Message = "Spreading Grass";
					for (var i = -Main.maxTilesX; i < Main.maxTilesX; i++)
					{
						var percent = (float) i / Main.maxTilesX;
						progress.Set(percent);
						WorldGen.SpreadGrass(i, Main.spawnTileY + 1);
					}
				}, 3f),
				new PassLegacy("Growing Trees", (progress, _) =>
				{
					progress.Message = "Growing Trees";
					WorldGen.AddTrees();
					progress.Set(1f);
				}, 3f),
				new PassLegacy("Growing Plants", (progress, _) =>
				{
					progress.Message = "Growing Plants";
					Tile tile;
					if (Main.halloween)
						for (var index129 = 40; index129 < Main.maxTilesX - 40; ++index129)
						for (var y = 50; y < Main.worldSurface; ++y)
						{
							tile = Main.tile[index129, y];
							if (tile.HasTile)
							{
								tile = Main.tile[index129, y];
								if (tile.TileType == 2 && WorldGen.genRand.Next(15) == 0)
								{
									WorldGen.PlacePumpkin(index129, y - 1);
									var num378 = WorldGen.genRand.Next(5);
									for (var index130 = 0; index130 < num378; ++index130)
										WorldGen.GrowPumpkin(index129, y - 1, 254);
								}
							}
						}

					for (var index = 0; index < Main.maxTilesX; ++index)
					{
						progress.Set(index / (float) Main.maxTilesX);
						for (var y = 1; y < Main.maxTilesY; ++y)
						{
							tile = Main.tile[index, y];
							if (tile.TileType == 2)
							{
								tile = Main.tile[index, y];
								if (tile.HasUnactuatedTile)
								{
									tile = Main.tile[index, y - 1];
									if (!tile.HasUnactuatedTile)
										WorldGen.PlaceTile(index, y - 1, 3, true);
									continue;
								}
							}

							tile = Main.tile[index, y];
							if (tile.TileType == 23)
							{
								tile = Main.tile[index, y];
								if (tile.HasUnactuatedTile)
								{
									tile = Main.tile[index, y - 1];
									if (!tile.HasTile)
										WorldGen.PlaceTile(index, y - 1, 24, true);
									continue;
								}
							}

							tile = Main.tile[index, y];
							if (tile.TileType == 199)
							{
								tile = Main.tile[index, y];
								if (tile.HasUnactuatedTile)
								{
									tile = Main.tile[index, y - 1];
									if (!tile.HasTile)
										WorldGen.PlaceTile(index, y - 1, 201, true);
								}
							}
						}
					}

					WorldGen.PlantAlch();
				}, 4f)
			};
		}
	}
}