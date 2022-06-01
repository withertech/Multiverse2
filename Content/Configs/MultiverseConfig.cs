using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Multiverse2.Content.Configs.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace Multiverse2.Content.Configs
{
	public class MultiverseConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[ReloadRequired]
		[DefaultValue(true)]
		[Label("Portal Recipe")]
		[Tooltip("Whether or not the Portal should have a recipe")]
		public bool PortalRecipe { get; set; }

		[ReloadRequired]
		[Tooltip("The list of worlds for Multiverse 2 to generate")]
		public List<MultiverseWorldConfiguration> Worlds { get; set; }

		[ReloadRequired]
		[Label("Tp Filter")]
		[Tooltip("The list of subworlds to filter from the /mvtp command and the portal tile")]
		public List<MultiverseTpFilterConfiguration> TpFilter { get; set; }
	}

	public class MultiverseWorldConfiguration
	{
		[DefaultValue("World")]
		[Tooltip("The world's name")] 
		public string Name { get; set; }

		[DefaultValue(0)]
		[Tooltip("The world's seed. If it is set to 0 (Default), then it will be random")]
		public int Seed { get; set; }

		[DefaultValue(true)]
		[Tooltip("Whether or not the world should save.")]
		public bool Saving { get; set; }

		[Range(4200, 8400)]
		[DefaultValue(4200)]
		[Slider]
		[Tooltip("The world's width")]
		public int Width { get; set; }

		[Range(1200, 2400)]
		[DefaultValue(1200)]
		[Slider]
		[Tooltip("The world's height")]
		public int Height { get; set; }

		[Tooltip("The world's generator")]
		public GeneratorDefinition Generator { get; set; }
	}

	public class MultiverseTpFilterConfiguration
	{
		[DefaultValue(false)]
		[Tooltip("Whether or not the subworld should be able to be traveled to using /mvtp")]
		public bool Command { get; set; }		
		
		[DefaultValue(false)]
		[Tooltip("Whether or not the subworld should be able to be traveled to using the portal tile")]
		public bool Portal { get; set; }

		[Tooltip("The subworld to filter")]
		public SubworldDefinition Subworld { get; set; }
	}
}