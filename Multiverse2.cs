using Terraria;
using Terraria.ModLoader;

namespace Multiverse2
{
	public class Multiverse2 : Mod
	{
		public override void Load()
		{
			ModLoader.TryGetMod("Wikithis", out Mod wikithis);
			if (wikithis != null && !Main.dedServ)
			{

				wikithis.Call(0, this, "terrariamods.fandom.com$Multiverse_2");
				wikithis.Call("AddModURL", this, "terrariamods.fandom.com$Multiverse_2");
			}
		}
	}
}