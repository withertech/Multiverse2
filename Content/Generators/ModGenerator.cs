using System;
using System.Collections.Generic;
using Multiverse2.Content.Configs;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public abstract class ModGenerator : ModType
	{
		public int Type { get; private set; }

		protected virtual int Seed => Main.rand.Next();
		
		public List<GenPass> GetPasses(MultiverseWorldConfiguration configuration)
		{
			var passes = GenPasses(SetupPasses(configuration));
			passes.Insert(0, new PassLegacy("Preview Start", (progress, gameConfiguration) =>
			{
				Main.refreshMap = true;
				Main.updateMap = false;
				Main.mapFullscreen = true;
				Main.mapStyle = 0;
				Main.mapReady = true;
				Main.mapEnabled = true;
			}));
			passes.Add(new PassLegacy("Preview Stop", (progress, gameConfiguration) =>
			{
				Main.mapFullscreen = false;
				Main.mapStyle = 1;
			}));
			return passes;
		}

		protected abstract List<GenPass> GenPasses(int seed);

		private int SetupPasses(MultiverseWorldConfiguration configuration)
		{
			int seed;
			if (configuration.Seed == 0)
			{
				seed = Seed;
				WorldGen._lastSeed = seed;
				WorldGen._genRand = new UnifiedRandom(seed);
				Main.rand = new UnifiedRandom(seed);
			}
			else
			{
				seed = configuration.Seed;
			}
			return seed;
		}
		
		protected sealed override void Register()
		{
			Type = GeneratorLoader.Add(this);
		}

		public sealed override void SetupContent()
		{
			SetStaticDefaults();
		}
	}
}