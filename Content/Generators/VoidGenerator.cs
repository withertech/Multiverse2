using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class VoidGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed)
		{
			return new()
			{
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
							WorldGen.SpreadGrass(i, j);
					}
				}),
				new PassLegacy("Growing Trees", (progress, _) =>
				{
					progress.Message = "Growing Trees";
					WorldGen.AddTrees();
					progress.Set(1f);
				}),
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
				})
			};
		}
	}
}