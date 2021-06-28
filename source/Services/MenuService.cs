using System.Collections.Generic;
using TownOfUs.PatchesArchive.CustomOption;

namespace TownOfUs.Services
{
    public class MenuService
    {
        public CustomOptionMenu CurrentMenu { get; }

        private Dictionary<int, CustomOptionMenu> RegisteredMenus { get; } = new Dictionary<int, CustomOptionMenu>();

        public void RegisterMenu(CustomOptionMenu menu)
        {
            if (!RegisteredMenus.ContainsKey(menu.Id))
                RegisteredMenus.Add(menu.Id, menu);
        }

        public void UnregisterMenu(int menuId)
        {
            RegisteredMenus.Remove(menuId);
        }

        public void SwitchScene(int menuId)
        {
        }
    }
}