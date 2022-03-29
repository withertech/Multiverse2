using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Multiverse2.Content.Generators
{
	public abstract class ModGenerator : ModType
	{
		public int Type { get; internal set; }

		public virtual int Seed => Main.rand.Next();
		
		public abstract List<GenPass> Passes { get; }

		protected sealed override void Register()
		{
			ModTypeLookup<ModGenerator>.Register(this);
			Type = GeneratorSystem.Add(this);
		}

		public sealed override void SetupContent()
		{
			SetStaticDefaults();
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name} ->\n" +
			       $"  {nameof(Type)}: {Type}";
		}
	}
}