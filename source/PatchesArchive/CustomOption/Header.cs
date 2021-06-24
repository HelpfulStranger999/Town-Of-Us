using TownOfUs.Extensions;

namespace TownOfUs.CustomOption
{
    public class CustomHeaderOption : CustomOptionBase
    {
        protected internal CustomHeaderOption(int id, string name) : base(name, CustomOptionType.Header, 0)
        {
        }

        public override void InitializeOption()
        {
            base.InitializeOption();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
        }

        public override OptionBehaviour Render()
        {
            Setting ??= UnityEngine.Object.Instantiate(TogglePrefab, TogglePrefab.transform.parent).DontDestroy();
            Setting.transform.GetChild(1).gameObject.SetActive(false);
            Setting.transform.GetChild(2).gameObject.SetActive(false);
            Setting.gameObject.SetActive(true);
            return Setting;
        }
    }
}