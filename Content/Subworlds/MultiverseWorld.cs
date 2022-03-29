using System.Collections.Generic;
using Multiverse2.Content.Configs;
using Multiverse2.Content.Generators;
using SubworldLibrary;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Subworlds
{
	public class MultiverseWorld : Subworld
	{
		private MultiverseWorldConfiguration Gen { get; }

		public override string Name => Gen.Name;

		public MultiverseWorld(MultiverseWorldConfiguration gen)
		{
			Gen = gen;
		}

		public override bool ShouldSave => true;

		public override bool NormalUpdates => true;

		public override int Width => Gen.Width;

		public override int Height => Gen.Height;

		public override List<GenPass> Tasks => GeneratorSystem.GetGenerator(Gen.Generator.Type).Passes;
	}
}