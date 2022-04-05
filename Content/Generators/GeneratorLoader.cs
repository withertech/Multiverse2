using System.Collections.Generic;
using Terraria.ModLoader;

namespace Multiverse2.Content.Generators
{
	public class GeneratorLoader
	{
		internal static List<ModGenerator> Generators = new();

		public static int GeneratorCount => Generators.Count;

		public static int Add(ModGenerator generator)
		{
			ModTypeLookup<ModGenerator>.Register(generator);
			Generators.Add(generator);
			return GeneratorCount - 1;
		}

		internal static ModGenerator Get(int type)
		{
			return Generators[type];
		}
	}
}