using System;
using System.Collections.Generic;

namespace TownOfUs.CustomOption
{
    public abstract class CustomOptionBase
    {
        protected static ToggleOption TogglePrefab = UnityEngine.Object.FindObjectOfType<ToggleOption>();
        protected static NumberOption NumberPrefab = UnityEngine.Object.FindObjectOfType<NumberOption>();
        protected static StringOption StringPrefab = UnityEngine.Object.FindObjectOfType<StringOption>();

        private static int GlobalUniqueId = 0;
        public static List<CustomOptionBase> AllOptions = new List<CustomOptionBase>();
        public readonly int ID;

        public Func<object, string> Format;
        public string Name;

        protected CustomOptionBase(string name, CustomOptionType type, object defaultValue,
            Func<object, string>? format = null)
        {
            ID = GlobalUniqueId++;
            Name = name;
            Type = type;
            DefaultValue = Value = defaultValue;
            Format = format ?? (obj => $"{obj}");

            if (Type == CustomOptionType.Button) return;
            AllOptions.Add(this);
            Set(Value);
        }

        protected internal object Value { get; set; }
        protected internal OptionBehaviour Setting { get; set; }
        protected internal CustomOptionType Type { get; set; }
        public object DefaultValue { get; set; }

        public static bool LobbyTextScroller { get; set; } = true;

        protected internal bool IsIndented { get; set; }

        public override string ToString()
        {
            return Format(Value);
        }

        public virtual void InitializeOption()
        {
            // Back this into render?
            Setting.name = Setting.gameObject.name = Name;
        }

        public abstract OptionBehaviour Render();

        protected internal void Set(object value, bool SendRpc = true)
        {
            PluginSingleton<TownOfUs>.Instance.Log.LogDebug($"{Name} set to {value}");

            Value = value;

            if (Setting != null && AmongUsClient.Instance.AmHost && SendRpc) Rpc.SendRpc(this);

            try
            {
                if (Setting is ToggleOption toggle)
                {
                    var newValue = (bool)Value;
                    toggle.oldValue = newValue;
                    if (toggle.CheckMark != null) toggle.CheckMark.enabled = newValue;
                }
                else if (Setting is NumberOption number)
                {
                    var newValue = (float)Value;

                    number.Value = number.oldValue = newValue;
                    number.ValueText.text = ToString();
                }
                else if (Setting is StringOption str)
                {
                    var newValue = (int)Value;

                    str.Value = str.oldValue = newValue;
                    str.ValueText.text = ToString();
                }
            }
            catch
            {
            }
        }
    }
}