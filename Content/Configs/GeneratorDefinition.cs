using System;
using System.ComponentModel;
using Multiverse2.Content.Generators;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace Multiverse2.Content.Configs
{
	[TypeConverter(typeof(ToFromStringConverter<GeneratorDefinition>))]
	public class GeneratorDefinition : EntityDefinition
	{
		public static readonly Func<TagCompound, GeneratorDefinition> DESERIALIZER = Load;

		public GeneratorDefinition()
		{
		}

		public GeneratorDefinition(int type)
			: base(GeneratorLoader.Get(type).FullName)
		{
		}

		public GeneratorDefinition(string key)
			: base(key)
		{
		}

		public GeneratorDefinition(string mod, string name)
			: base(mod, name)
		{
		}

		public override int Type =>
			!ModContent.TryFind<ModGenerator>(mod != "Terraria" ? mod + "/" + name : name, out var gen) ? 1 : gen.Type;

		public static GeneratorDefinition FromString(string s)
		{
			return new(s);
		}

		public static GeneratorDefinition Load(TagCompound tag)
		{
			return new(tag.GetString("mod"), tag.GetString("name"));
		}
	}
}