using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
		public bool PortalRecipe;

		[ReloadRequired]
		 [Tooltip("The list of worlds for Multiverse 2 to generate")]
		public List<MultiverseWorldConfiguration> Worlds;

		[ReloadRequired]
		[Label("Tp Filter")]
		[Tooltip("The list of subworlds to filter from the /mvtp command and the portal tile")]
		public List<MultiverseTpFilterConfiguration> TpFilter;
	}

	public class MultiverseWorldConfiguration
	{
		[DefaultValue("World")] [Tooltip("The world's name")]
		public string Name;

		[DefaultValue(0)] [Tooltip("The world's seed. If it is set to 0 (Default), then it will be random")]
		public int Seed;

		[DefaultValue(true)] [Tooltip("Whether or not the world should save.")]
		public bool Saving;

		[Range(4200, 8400)] [DefaultValue(4200)] [Slider] [Tooltip("The world's width")]
		public int Width;

		[Range(1200, 2400)] [DefaultValue(1200)] [Slider] [Tooltip("The world's height")]
		public int Height;

		[Tooltip("The world's generator")] public GeneratorDefinition Generator;

		protected bool Equals(MultiverseWorldConfiguration other)
		{
			return Name == other.Name && Seed == other.Seed && Saving == other.Saving && Width == other.Width && Height == other.Height && Equals(Generator, other.Generator);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MultiverseWorldConfiguration)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Seed, Saving, Width, Height, Generator);
		}
	}

	public class MultiverseTpFilterConfiguration
	{
		[DefaultValue(true)] [Tooltip("Whether or not the subworld should be able to be traveled to using /mvtp")]
		public bool Command;

		[DefaultValue(true)]
		[Tooltip("Whether or not the subworld should be able to be traveled to using the portal tile")]
		public bool Portal;

		[Tooltip("The subworld to filter")] public SubworldDefinition Subworld;

		protected bool Equals(MultiverseTpFilterConfiguration other)
		{
			return Command == other.Command && Portal == other.Portal && Equals(Subworld, other.Subworld);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MultiverseTpFilterConfiguration)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Command, Portal, Subworld);
		}
	}
}