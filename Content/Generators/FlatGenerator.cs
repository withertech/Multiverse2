using System.Collections.Generic;
using Multiverse2.Content.Configs;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class FlatGenerator : ModGenerator
	{
		protected override List<GenPass> GenPasses(int seed) => new()
		{
			new PassLegacy("Spawn", (progress, _) =>
			{
				Main.spawnTileX = Main.maxTilesX / 2;
				Main.spawnTileY = Main.maxTilesY / 2;
			}),
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
			}),
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
			}),
			new PassLegacy("Shinies", (progress, _) =>
			{
				progress.Message = Lang.gen[16].Value;
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

			}),
			new PassLegacy("Spreading Grass", (progress, _) =>
			{
				progress.Message = "Spreading Grass";
				for (var i = -Main.maxTilesX; i < Main.maxTilesX; i++)
				{
					var percent = (float) i / Main.maxTilesX;
					progress.Set(percent);
					WorldGen.SpreadGrass(i, Main.spawnTileY + 1);
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