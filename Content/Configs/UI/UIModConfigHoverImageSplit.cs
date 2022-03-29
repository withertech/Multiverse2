// Decompiled with JetBrains decompiler
// Type: Terraria.ModLoader.Config.UI.UIModConfigHoverImageSplit
// Assembly: tModLoader, Version=1.4.3.6, Culture=neutral, PublicKeyToken=null
// MVID: 1631351A-60C2-4B39-9001-BE94582C6087
// Assembly location: G:\SteamLibrary\steamapps\common\tModLoader\tModLoader.dll

using System;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace Multiverse2.Content.Configs.UI
{
	public class UIModConfigHoverImageSplit : UIImage
	{
		internal string HoverTextDown;
		internal string HoverTextUp;

		public UIModConfigHoverImageSplit(
			Asset<Texture2D> texture,
			string hoverTextUp,
			string hoverTextDown)
			: base(texture)
		{
			HoverTextUp = hoverTextUp;
			HoverTextDown = hoverTextDown;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var rectangle = GetDimensions().ToRectangle();
			if (!IsMouseHovering)
				return;
			var fieldInfo = Type.GetType("Terraria.ModLoader.Config.UI.UIModConfig")
				?.GetField("tooltip", BindingFlags.Static);

			fieldInfo?.SetValue(null,
				Main.mouseY < rectangle.Y + rectangle.Height / 2 ? HoverTextUp : HoverTextDown);
		}
	}
}