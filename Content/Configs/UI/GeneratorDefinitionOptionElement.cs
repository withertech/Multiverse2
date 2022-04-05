using System;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Multiverse2.Content.Generators;
using Terraria.ModLoader.UI;

namespace Multiverse2.Content.Configs.UI
{
	public class GeneratorDefinitionOptionElement : DefinitionOptionElement<GeneratorDefinition>
	{
		private readonly UIAutoScaleTextTextPanel<string> _text;

		public GeneratorDefinitionOptionElement(GeneratorDefinition definition, float scale = 0.75f)
			: base(definition, scale)
		{
			Width.Set(150f * scale, 0.0f);
			Height.Set(40f * scale, 0.0f);
			var scaleTextTextPanel =
				new UIAutoScaleTextTextPanel<string>(type == -1 ? "None" : GeneratorLoader.Get(type).Name);
			scaleTextTextPanel.Width.Percent = 1f;
			scaleTextTextPanel.Height.Percent = 1f;
			_text = scaleTextTextPanel;
			Append(_text);
		}

		public override void SetItem(GeneratorDefinition item)
		{
			base.SetItem(item);
			_text?.SetText(type == -1 ? "None" : GeneratorLoader.Get(type).Name);
		}

		public override void SetScale(float scale)
		{
			base.SetScale(scale);
			Width.Set(150f * scale, 0.0f);
			Height.Set(40f * scale, 0.0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (!IsMouseHovering)
				return;
			var fieldInfo = Type.GetType("Terraria.ModLoader.Config.UI.UIModConfig")
				?.GetField("tooltip", BindingFlags.Static);
			fieldInfo?.SetValue(null, tooltip);
		}
	}
}