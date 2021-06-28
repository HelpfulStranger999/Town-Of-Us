using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.CustomOption
{
    public class CustomStringOption : CustomOptionBase<string>
    {
        protected internal CustomStringOption(int id, string name, string[] values) : base(id, name,
            CustomOptionType.String,
            0)
        {
            Values = values;
            Format = value => Values[(int)value];
        }

        protected string[] Values { get; set; }

        protected internal int Get()
        {
            return (int)RawValue;
        }

        protected internal void Increase()
        {
            Set(Mathf.Clamp(Get() + 1, 0, Values.Length - 1));
        }

        protected internal void Decrease()
        {
            Set(Mathf.Clamp(Get() - 1, 0, Values.Length - 1));
        }

        public override void InitializeOption()
        {
            var str = Setting.Cast<StringOption>();

            str.TitleText.text = Name;
            str.Value = str.oldValue = Get();
            str.ValueText.text = ToString();
        }

        public override OptionBehaviour Render()
        {
            if (Setting == null)
            {
                Setting = Object.Instantiate(StringPrefab, StringPrefab.transform.parent).DontDestroy();
            }
            else
            {
                Setting.gameObject.SetActive(true);
            }
            return Setting;
        }
    }
}