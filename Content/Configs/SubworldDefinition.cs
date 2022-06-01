using System;
using System.ComponentModel;
using System.Linq;
using Multiverse2.Content.Configs.UI;
using SubworldLibrary;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace Multiverse2.Content.Configs
{
	[TypeConverter(typeof(ToFromStringConverter<SubworldDefinition>))]
	[CustomModConfigItem(typeof(SubworldDefinitionElement))]
	public class SubworldDefinition : EntityDefinition
	{
		public static readonly Func<TagCompound, SubworldDefinition> DESERIALIZER = Load;

		public SubworldDefinition()
			: this(-1)
		{
		}

		public SubworldDefinition(int type)
			: base(type >= 0? ModContent.GetContent<Subworld>().ToList()[type].FullName : "Terraria/None")
		{
		}

		public SubworldDefinition(string key)
			: base(key)
		{
		}

		public SubworldDefinition(string mod, string name)
			: base(mod, name)
		{
		}

		public override int Type => !ModContent.TryFind<Subworld>($"{Mod}/{Name}", out var subworld)
			? -1
			: ModContent.GetContent<Subworld>().ToList().IndexOf(subworld);

		public static SubworldDefinition FromString(string s)
		{
			return new SubworldDefinition(s);
		}

		public static SubworldDefinition Load(TagCompound tag)
		{
			return new SubworldDefinition(tag.GetString("mod"), tag.GetString("name"));
		}
	}
}