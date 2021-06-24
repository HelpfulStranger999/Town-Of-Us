using System.Collections.Generic;
using TownOfUs.CustomOption;

namespace TownOfUs.PatchesArchive.CustomOption
{
    public class CustomOptionMenu
    {
        public IReadOnlyCollection<CustomOptionBase> MenuItems => Options.AsReadOnly();
        private List<CustomOptionBase> Options { get; } = new List<CustomOptionBase>();

        public CustomOptionMenu()
        { }

        public void RegisterMenuItem(CustomOptionBase option)
        {
            Options.Add(option);
        }

        public OptionsMenuBehaviour[] RenderMenu(float x, float minimumY, float z)
        {
            var options = new List<OptionsMenuBehaviour>();

            return options.ToArray();
        }
    }
}