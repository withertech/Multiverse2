using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public class VanillaGenerator : ModGenerator
	{
		public override List<GenPass> Passes
		{
			get
			{
				var passes = new List<GenPass>
				{
					new PassLegacy("Vanilla", (progress, _) => WorldGen.GenerateWorld(Seed, progress))
				};
				return passes;
			}
		}
	}
}