using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.CustomOption
{
    public class CustomToggleOption : CustomOptionBase
    {
        protected internal CustomToggleOption(int id, string name, bool value = true) : base(id, name,
            CustomOptionType.Toggle,
            value)
        {
            Format = val => (bool)val ? "On" : "Off";
        }

        protected internal bool Get()
        {
            return (bool)Value;
        }

        protected internal void Toggle()
        {
            Set(!Get());
        }

        public override void InitializeOption()
        {
            base.InitializeOption();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
            Setting.Cast<ToggleOption>().CheckMark.enabled = Get();
        }

        public override OptionBehaviour Render()
        {
            Setting ??= Object.Instantiate(TogglePrefab, TogglePrefab.transform.parent).DontDestroy();
            Setting.gameObject.SetActive(true);
            return Setting;
        }
    }
}