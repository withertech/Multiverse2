using System.Collections.Generic;
using Multiverse2.Content.Configs;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public abstract class ModGenerator : ModType
	{
		private static readonly UnifiedRandom Rand = new();

		public int Type { get; private set; }

		protected virtual int Seed => Rand.Next();

		public List<GenPass> GetPasses(MultiverseWorldConfiguration configuration)
		{
			return GenPasses(SetupPasses(configuration));
		}

		protected abstract List<GenPass> GenPasses(int seed);

		private int SetupPasses(MultiverseWorldConfiguration configuration)
		{
			var seed = configuration.Seed == 0 ? Seed : configuration.Seed;
			WorldGen._lastSeed = seed;
			WorldGen._genRand = new UnifiedRandom(seed);
			Main.rand = new UnifiedRandom(seed);
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