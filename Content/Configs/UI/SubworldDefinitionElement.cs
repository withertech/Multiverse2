using System;
using System.Collections.Generic;
using System.Linq;
using SubworldLibrary;
using Terraria.ModLoader;

namespace Multiverse2.Content.Configs.UI
{
	public class SubworldDefinitionElement : DefinitionElement<SubworldDefinition>
	{
		public Predicate<SubworldDefinition> Filter { get; }

		public SubworldDefinitionElement()
			: this(_ => true)
		{
			
		}

		public SubworldDefinitionElement(Predicate<SubworldDefinition> filter)
		{
			Filter = filter;
		}
		protected override DefinitionOptionElement<SubworldDefinition> CreateDefinitionOptionElement()
		{
			return new SubworldDefinitionOptionElement(Value, 0.8f);
		}

		protected override void TweakDefinitionOptionElement(
			DefinitionOptionElement<SubworldDefinition> optionElement)
		{
			optionElement.Top.Set(0.0f, 0.0f);
			optionElement.Left.Set(-124f, 1f);
		}

		protected override List<DefinitionOptionElement<SubworldDefinition>> CreateDefinitionOptionElementList()
		{
			optionScale = 0.8f;
			var definitionOptionElementList = new List<DefinitionOptionElement<SubworldDefinition>>();
			for (var type = 0; type < ModContent.GetContent<Subworld>().ToList().Count; ++type)
			{
				var definition = type != -1
					? new SubworldDefinition(type)
					: new SubworldDefinition("Terraria", "None");
				if (!Filter.Invoke(definition)) continue;
				var optionElement = new SubworldDefinitionOptionElement(definition, optionScale);
						
				optionElement.OnClick += (a, b) =>
				{
					Value = optionElement.definition;
					updateNeeded = true;
					selectionExpanded = false;
				};
				definitionOptionElementList.Add(optionElement);
			}

			return definitionOptionElementList;
		}

		protected override List<DefinitionOptionElement<SubworldDefinition>> GetPassedOptionElements()
		{
			var definitionOptionElementList = new List<DefinitionOptionElement<SubworldDefinition>>();
			foreach (var option in options)
				if (ModContent.GetContent<Subworld>().ToList()[option.type].Name.IndexOf(chooserFilter.CurrentString,
					    StringComparison.OrdinalIgnoreCase) !=
				    -1)
				{
					var str = option.definition.Mod;
					if (option.type >= 0)
						str = ModContent.GetContent<Subworld>().ToList()[option.type].Mod.DisplayName;
					if (str.IndexOf(chooserFilterMod.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
						definitionOptionElementList.Add(option);
				}

			return definitionOptionElementList;
		}
	}
}