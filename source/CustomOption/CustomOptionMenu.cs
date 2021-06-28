using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TownOfUs.CustomOption;
using UnityEngine;

namespace TownOfUs.PatchesArchive.CustomOption
{
    public class CustomOptionMenu
    {
        public IReadOnlyCollection<CustomOptionBase> MenuItems => Options.Values.ToImmutableList();
        private Dictionary<int, CustomOptionBase> Options { get; } = new Dictionary<int, CustomOptionBase>();

        private static int GlobalUniqueId = 0;
        public int Id { get; }

        public CustomOptionMenu()
        {
            Id = GlobalUniqueId++;
        }

        public void RegisterMenuItem(CustomOptionBase option)
        {
            Options.Add(option.Id, option);
        }

        public CustomOptionBase FindOption(OptionBehaviour option)
        {
            return Options.Values.Single(opt => opt.Setting.Equals(option));
        }

        public TOption? FindOption<TOption>(OptionBehaviour option) where TOption : CustomOptionBase
        {
            return FindOption(option) as TOption;
        }

        public OptionBehaviour[] RenderMenu(Vector3 initialPosition)
        {
            var options = new List<OptionBehaviour>();

            var i = 0;
            foreach (var option in Options.Values)
            {
                var optionBehavior = option.Render();
                optionBehavior.transform.position = initialPosition - new Vector3(0, 0.5f * i++, 0);
                options.Add(optionBehavior);
            }

            return options.ToArray();
        }
    }
}