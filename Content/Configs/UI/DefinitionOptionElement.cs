// Decompiled with JetBrains decompiler
// Type: Terraria.ModLoader.Config.UI.DefinitionOptionElement`1
// Assembly: tModLoader, Version=1.4.3.6, Culture=neutral, PublicKeyToken=null
// MVID: 1631351A-60C2-4B39-9001-BE94582C6087
// Assembly location: G:\SteamLibrary\steamapps\common\tModLoader\tModLoader.dll

using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace Multiverse2.Content.Configs.UI
{
	public class DefinitionOptionElement<T> : UIElement where T : EntityDefinition
	{
		public static Asset<Texture2D> defaultBackgroundTexture = TextureAssets.InventoryBack9;
		public Asset<Texture2D> backgroundTexture = defaultBackgroundTexture;
		public T definition;
		internal float scale = 0.75f;
		public string tooltip;
		public int type;
		protected bool unloaded;

		public DefinitionOptionElement(T definition, float scale = 0.75f)
		{
			SetItem(definition);
			this.scale = scale;
			Width.Set(defaultBackgroundTexture.Width() * scale, 0.0f);
			Height.Set(defaultBackgroundTexture.Height() * scale, 0.0f);
		}

		public virtual void SetItem(T item)
		{
			definition = item;
			type = definition?.Type ?? 0;
			unloaded = definition is {IsUnloaded: true};
			if (definition == null || type == 0 && !unloaded)
			{
				tooltip = "Nothing";
			}
			else
			{
				var interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
				interpolatedStringHandler.AppendFormatted(definition.name);
				interpolatedStringHandler.AppendLiteral(" [");
				interpolatedStringHandler.AppendFormatted(definition.mod);
				interpolatedStringHandler.AppendLiteral("]");
				interpolatedStringHandler.AppendFormatted(unloaded
					? " (" + Language.GetTextValue("tModLoader.UnloadedItemItemName") + ")"
					: "");
				tooltip = interpolatedStringHandler.ToStringAndClear();
			}
		}

		public virtual void SetScale(float scale)
		{
			this.scale = scale;
			Width.Set(defaultBackgroundTexture.Width() * scale, 0.0f);
			Height.Set(defaultBackgroundTexture.Height() * scale, 0.0f);
		}

		public override int CompareTo(object obj)
		{
			return type.CompareTo((obj as DefinitionOptionElement<T>).type);
		}
	}
}