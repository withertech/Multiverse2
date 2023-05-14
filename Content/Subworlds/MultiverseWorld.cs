using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Multiverse2.Content.Configs;
using Multiverse2.Content.Generators;
using SubworldLibrary;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Subworlds
{
	public class MultiverseWorld : Subworld
	{
		private UIWorldLoad _menu;

		public MultiverseWorld(MultiverseWorldConfiguration gen)
		{
			Gen = gen;
		}

		private MultiverseWorldConfiguration Gen { get; }

		public override string Name => Gen.Name.Replace(" ", "");

		public override bool ShouldSave => Gen.Saving;

		public override bool NormalUpdates => true;

		public override int Width => Gen.Width;

		public override int Height => Gen.Height;

		public override WorldGenConfiguration Config => WorldGenConfiguration.FromEmbeddedPath("Terraria.GameContent.WorldBuilding.Configuration.json");

		public override List<GenPass> Tasks => GeneratorLoader.Get(Gen.Generator.Type).GetPasses(Gen, Config);

		public override void DrawMenu(GameTime gameTime)
		{
			if (WorldGenerator.CurrentGenerationProgress != null)
				(_menu ??= new UIWorldLoad()).Draw(Main.spriteBatch);
			else
				base.DrawMenu(gameTime);
		}
	}
}
