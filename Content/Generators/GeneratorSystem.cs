using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Multiverse2.Content.Generators
{
	public class GeneratorSystem : ModSystem
	{
		internal static List<ModGenerator> Generators = new();
		
		public static int GeneratorCount => Generators.Count;

		public static int Add(ModGenerator generator)
		{
			Generators.Add(generator);
			return GeneratorCount - 1;
		}

		internal static ModGenerator GetGenerator(int type)
		{
			return Generators[type];
		}
	}
}