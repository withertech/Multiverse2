using System;
using System.ComponentModel;
using System.Linq;
using SubworldLibrary;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace Multiverse2.Content.Configs
{
	[TypeConverter(typeof(ToFromStringConverter<SubworldDefinition>))]
	public class SubworldDefinition : EntityDefinition
	{
		public static readonly Func<TagCompound, SubworldDefinition> DESERIALIZER = Load;

		public SubworldDefinition()
		{
		}

		public SubworldDefinition(int type)
			: base(ModContent.GetContent<Subworld>().ToList()[type].FullName)
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

		public override int Type => !ModContent.TryFind<Subworld>($"{mod}/{name}", out var subworld)
			? 0
			: ModContent.GetContent<Subworld>().ToList().IndexOf(subworld);

		public static SubworldDefinition FromString(string s)
		{
			return new(s);
		}

		public static SubworldDefinition Load(TagCompound tag)
		{
			return new(tag.GetString("mod"), tag.GetString("name"));
		}
	}
}