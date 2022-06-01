using System.Collections.Generic;
using System.ComponentModel;
using Multiverse2.Content.Configs.UI;
using Terraria.ModLoader.Config;

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
	}

	public class MultiverseWorldConfiguration
	{
		[Tooltip("The world's name")] public string Name { get; set; }

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

		[CustomModConfigItem(typeof(GeneratorDefinitionElement))]
		[Tooltip("The world's generator")]
		public GeneratorDefinition Generator { get; set; } = new("Multiverse2/VanillaGenerator");
	}
}