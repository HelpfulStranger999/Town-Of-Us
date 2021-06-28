using TownOfUs.Extensions;
using UnityEngine;
using UEObject = UnityEngine.Object;

namespace TownOfUs.CustomOption
{
    public enum ButtonState
    {
        Unclicked,
        Clicked
    }

    public abstract class CustomButtonOption : CustomOptionBase
    {
        protected internal CustomButtonOption(string name)
            : base(name, CustomOptionType.Button, ButtonState.Unclicked)
        {
        }

        public abstract void Click();

        public override void InitializeOption()
        {
            base.InitializeOption();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
        }

        public override OptionBehaviour Render()
        {
            if (Setting == null)
            {
                Setting = UEObject.Instantiate(TogglePrefab, TogglePrefab.transform.parent).DontDestroy();
                Setting.transform.GetChild(2).gameObject.SetActive(false);
                Setting.transform.GetChild(0).localPosition += new Vector3(1f, 0f, 0f);
            }
            else
            {
                Setting.gameObject.SetActive(true);
                InitializeOption();
            }
            return Setting;
        }
    }
}