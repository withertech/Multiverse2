// Decompiled with JetBrains decompiler
// Type: Terraria.ModLoader.UI.Elements.NestedUIGrid
// Assembly: tModLoader, Version=1.4.3.6, Culture=neutral, PublicKeyToken=null
// MVID: 1631351A-60C2-4B39-9001-BE94582C6087
// Assembly location: G:\SteamLibrary\steamapps\common\tModLoader\tModLoader.dll

using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace Multiverse2.Content.Configs.UI
{
	public class NestedUIGrid : UIGrid
	{
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			if (_scrollbar != null)
			{
				double viewPosition1 = _scrollbar.ViewPosition;
				_scrollbar.ViewPosition -= evt.ScrollWheelValue;
				double viewPosition2 = _scrollbar.ViewPosition;
				if (viewPosition1 != viewPosition2)
					return;
				base.ScrollWheel(evt);
			}
			else
			{
				base.ScrollWheel(evt);
			}
		}
	}
}