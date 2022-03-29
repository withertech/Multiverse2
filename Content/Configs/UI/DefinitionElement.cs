// Decompiled with JetBrains decompiler
// Type: Terraria.ModLoader.Config.UI.DefinitionElement`1
// Assembly: tModLoader, Version=1.4.3.6, Culture=neutral, PublicKeyToken=null
// MVID: 1631351A-60C2-4B39-9001-BE94582C6087
// Assembly location: G:\SteamLibrary\steamapps\common\tModLoader\tModLoader.dll

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace Multiverse2.Content.Configs.UI
{
	public abstract class DefinitionElement<T> : ConfigElement<T> where T : EntityDefinition
	{
		protected UIFocusInputTextField chooserFilter;
		protected UIFocusInputTextField chooserFilterMod;
		protected NestedUIGrid chooserGrid;
		protected UIPanel chooserPanel;
		protected DefinitionOptionElement<T> optionChoice;
		protected List<DefinitionOptionElement<T>> options;
		protected float optionScale = 0.5f;
		protected bool selectionExpanded;
		protected bool updateNeeded;

		public override void OnBind()
		{
			base.OnBind();
			Height.Set(30f, 0.0f);
			optionChoice = CreateDefinitionOptionElement();
			optionChoice.Top.Set(2f, 0.0f);
			optionChoice.Left.Set(-30f, 1f);
			optionChoice.OnClick += (a, b) =>
			{
				selectionExpanded = !selectionExpanded;
				updateNeeded = true;
			};
			TweakDefinitionOptionElement(optionChoice);
			Append(optionChoice);
			chooserPanel = new UIPanel();
			chooserPanel.Top.Set(30f, 0.0f);
			chooserPanel.Height.Set(200f, 0.0f);
			chooserPanel.Width.Set(0.0f, 1f);
			chooserPanel.BackgroundColor = Color.CornflowerBlue;
			var element1 = new UIPanel();
			element1.Width.Set(160f, 0.0f);
			element1.Height.Set(30f, 0.0f);
			element1.Top.Set(-6f, 0.0f);
			element1.PaddingTop = 0.0f;
			element1.PaddingBottom = 0.0f;
			chooserFilter = new UIFocusInputTextField("Filter by Name");
			chooserFilter.OnTextChange += (a, b) => updateNeeded = true;
			chooserFilter.OnRightClick += (a, b) => chooserFilter.SetText("");
			chooserFilter.Width = StyleDimension.Fill;
			chooserFilter.Height.Set(-6f, 1f);
			chooserFilter.Top.Set(6f, 0.0f);
			element1.Append(chooserFilter);
			chooserPanel.Append(element1);
			var element2 = new UIPanel();
			element2.CopyStyle(element1);
			element2.Left.Set(180f, 0.0f);
			chooserFilterMod = new UIFocusInputTextField("Filter by Mod");
			chooserFilterMod.OnTextChange += (a, b) => updateNeeded = true;
			chooserFilterMod.OnRightClick += (a, b) => chooserFilterMod.SetText("");
			chooserFilterMod.Width = StyleDimension.Fill;
			chooserFilterMod.Height.Set(-6f, 1f);
			chooserFilterMod.Top.Set(6f, 0.0f);
			element2.Append(chooserFilterMod);
			chooserPanel.Append(element2);
			chooserGrid = new NestedUIGrid();
			chooserGrid.Top.Set(30f, 0.0f);
			chooserGrid.Height.Set(-30f, 1f);
			chooserGrid.Width.Set(-12f, 1f);
			chooserPanel.Append(chooserGrid);
			var uiScrollbar = new UIScrollbar();
			uiScrollbar.SetView(100f, 1000f);
			uiScrollbar.Height.Set(-30f, 1f);
			uiScrollbar.Top.Set(30f, 0.0f);
			uiScrollbar.Left.Pixels += 8f;
			uiScrollbar.HAlign = 1f;
			chooserGrid.SetScrollbar(uiScrollbar);
			chooserPanel.Append(uiScrollbar);
			var element3 = new UIModConfigHoverImageSplit(upDownTexture, "Zoom in", "Zoom out");
			element3.Recalculate();
			element3.Top.Set(-4f, 0.0f);
			element3.Left.Set(-18f, 1f);
			element3.OnClick += (a, b) =>
			{
				var rectangle = b.GetDimensions().ToRectangle();
				optionScale = a.MousePosition.Y >= (double) (rectangle.Y + rectangle.Height / 2)
					? Math.Max(0.5f, optionScale - 0.1f)
					: Math.Min(1f, optionScale + 0.1f);
				foreach (var option in options)
					option.SetScale(optionScale);
			};
			chooserPanel.Append(element3);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!updateNeeded)
				return;
			updateNeeded = false;
			if (selectionExpanded && options == null)
				options = CreateDefinitionOptionElementList();
			if (!selectionExpanded)
				chooserPanel.Remove();
			else
				Append(chooserPanel);
			var pixels = selectionExpanded ? 240f : 30f;
			Height.Set(pixels, 0.0f);
			if (Parent != null && Parent is UISortableElement)
				Parent.Height.Pixels = pixels;
			if (selectionExpanded)
			{
				var passedOptionElements = GetPassedOptionElements();
				chooserGrid.Clear();
				chooserGrid.AddRange(passedOptionElements);
			}

			optionChoice.SetItem(Value);
		}

		protected abstract List<DefinitionOptionElement<T>> GetPassedOptionElements();

		protected abstract List<DefinitionOptionElement<T>> CreateDefinitionOptionElementList();

		protected abstract DefinitionOptionElement<T> CreateDefinitionOptionElement();

		protected virtual void TweakDefinitionOptionElement(DefinitionOptionElement<T> optionElement)
		{
		}
	}
}