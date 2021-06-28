using TownOfUs.Services;

namespace TownOfUs.CustomOption
{
    public class CustomMenuNavigationButton : CustomButtonOption
    {
        private MenuService MenuService { get; }
        private int TargetMenu { get; }

        public CustomMenuNavigationButton(string name, MenuService menuService, int targetMenu) : base(name)
        {
            MenuService = menuService;
            TargetMenu = targetMenu;
        }

        public override void Click()
        {
            MenuService.SwitchScene(TargetMenu);
        }
    }
}