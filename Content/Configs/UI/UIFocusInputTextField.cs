// Decompiled with JetBrains decompiler
// Type: Terraria.ModLoader.UI.UIFocusInputTextField
// Assembly: tModLoader, Version=1.4.3.6, Culture=neutral, PublicKeyToken=null
// MVID: 1631351A-60C2-4B39-9001-BE94582C6087
// Assembly location: G:\SteamLibrary\steamapps\common\tModLoader\tModLoader.dll

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace Multiverse2.Content.Configs.UI
{
	public class UIFocusInputTextField : UIElement
	{
		public delegate void EventHandler(object sender, EventArgs e);

		private readonly string _hintText;
		private int _textBlinkerCount;
		private int _textBlinkerState;
		internal string CurrentString = "";
		internal bool Focused;

		public UIFocusInputTextField(string hintText)
		{
			_hintText = hintText;
		}

		public bool UnfocusOnTab { get; internal set; }

		public event EventHandler OnTextChange;

		public event EventHandler OnUnfocus;

		public event EventHandler OnTab;

		public void SetText(string text)
		{
			if (text == null)
				text = "";
			if (!(CurrentString != text))
				return;
			CurrentString = text;
			var onTextChange = OnTextChange;
			if (onTextChange == null)
				return;
			onTextChange(this, new EventArgs());
		}

		public override void Click(UIMouseEvent evt)
		{
			Main.clrInput();
			Focused = true;
		}

		public override void Update(GameTime gameTime)
		{
			if (!ContainsPoint(new Vector2(Main.mouseX, Main.mouseY)) && Main.mouseLeft)
			{
				Focused = false;
				var onUnfocus = OnUnfocus;
				if (onUnfocus != null)
					onUnfocus(this, new EventArgs());
			}

			base.Update(gameTime);
		}

		private static bool JustPressed(Keys key)
		{
			return Main.inputText.IsKeyDown(key) && !Main.oldInputText.IsKeyDown(key);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Focused)
			{
				PlayerInput.WritingText = true;
				Main.instance.HandleIME();
				var inputText = Main.GetInputText(CurrentString);
				if (!inputText.Equals(CurrentString))
				{
					CurrentString = inputText;
					var onTextChange = OnTextChange;
					if (onTextChange != null)
						onTextChange(this, new EventArgs());
				}
				else
				{
					CurrentString = inputText;
				}

				if (JustPressed(Keys.Tab))
				{
					if (UnfocusOnTab)
					{
						Focused = false;
						var onUnfocus = OnUnfocus;
						if (onUnfocus != null)
							onUnfocus(this, new EventArgs());
					}

					var onTab = OnTab;
					if (onTab != null)
						onTab(this, new EventArgs());
				}

				if (++_textBlinkerCount >= 20)
				{
					_textBlinkerState = (_textBlinkerState + 1) % 2;
					_textBlinkerCount = 0;
				}
			}

			var currentString = CurrentString;
			if (_textBlinkerState == 1 && Focused)
				currentString += "|";
			var dimensions = GetDimensions();
			if (CurrentString.Length == 0 && !Focused)
				Utils.DrawBorderString(spriteBatch, _hintText, new Vector2(dimensions.X, dimensions.Y), Color.Gray);
			else
				Utils.DrawBorderString(spriteBatch, currentString, new Vector2(dimensions.X, dimensions.Y),
					Color.White);
		}
	}
}