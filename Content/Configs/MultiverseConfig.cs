using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Multiverse2.Content.Configs.UI;
using Multiverse2.Content.Generators;
using Terraria.ModLoader.Config;

namespace Multiverse2.Content.Configs
{
	public class MultiverseConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[ReloadRequired]
		public List<MultiverseWorldConfiguration> Worlds = new();
	}

	public class MultiverseWorldConfiguration
	{
		public string Name { get; set; }
		
		[Range(4200, 8400)]
		[DefaultValue(4200)]
		[Slider]
		public int Width { get; set; }
		
		[Range(1200, 2400)]
		[DefaultValue(1200)]
		[Slider]
		public int Height { get; set; }

		[CustomModConfigItem(typeof(GeneratorDefinitionElement))]
		public GeneratorDefinition Generator { get; set; } = new();
	}
}