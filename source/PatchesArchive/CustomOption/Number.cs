using System;
using TownOfUs.Extensions;
using TownOfUs.PatchesArchive.CustomOption;
using UnityEngine;
using UEObject = UnityEngine.Object;

namespace TownOfUs.CustomOption
{
    public class CustomNumberOption : CustomOptionBase
    {
        protected Range<float> Range { get; }
        protected float Increment { get; set; }

        protected internal CustomNumberOption(string name, Range<float> range, float increment,
        Func<object, string> format = null) : base(name, CustomOptionType.Number, value, format)
        {
            Min = min;
            Max = max;
            Increment = increment;
        }

        protected internal CustomNumberOption(bool indent, string name, float value, float min, float max,
            float increment,
        Func<object, string> format = null) : this(name, value, min, max, increment, format)
        {
            IsIndented = indent;
        }

        protected internal float Get()
        {
            return (float)Value;
        }

        protected internal void Increase()
        {
            Set(Mathf.Clamp(Get() + Increment, Min, Max));
        }

        protected internal void Decrease()
        {
            Set(Mathf.Clamp(Get() - Increment, Min, Max));
        }

        public override void InitializeOption()
        {
            base.InitializeOption();
            var number = Setting.Cast<NumberOption>();

            number.TitleText.text = Name;
            number.ValidRange = new FloatRange(Min, Max);
            number.Increment = Increment;
            number.Value = number.oldValue = Get();
            number.ValueText.text = ToString();
        }

        public override OptionBehaviour Render()
        {
            Setting ??= UEObject.Instantiate(NumberPrefab, NumberPrefab.transform.parent).DontDestroy();
            Setting.gameObject.SetActive(true);
            return Setting;
        }
    }
}