using System;

namespace TownOfUs.CustomOption
{
    public abstract class CustomOptionBase<TValue> where TValue
    {
        protected static ToggleOption TogglePrefab = UnityEngine.Object.FindObjectOfType<ToggleOption>();
        protected static NumberOption NumberPrefab = UnityEngine.Object.FindObjectOfType<NumberOption>();
        protected static StringOption StringPrefab = UnityEngine.Object.FindObjectOfType<StringOption>();

        private static int GlobalUniqueId = 0;
        public readonly int Id;

        public Func<TValue, string> Format;
        public string Name;

        protected CustomOptionBase(string name, CustomOptionType type, TValue defaultValue,
            Func<TValue, string>? format = null)
        {
            Id = GlobalUniqueId++;
            Name = name;
            Type = type;
            RawValue = defaultValue;
            Format = format ?? (obj => $"{obj}");

            if (Type == CustomOptionType.Button) return;
            Set(Value);
        }

        public TValue Value => (TValue)RawValue;
        private object RawValue { get; set; }
        public OptionBehaviour Setting { get; protected set; }
        public CustomOptionType Type { get; protected set; }

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

        protected internal void Set(TValue value, bool SendRpc = true)
        {
            PluginSingleton<TownOfUs>.Instance.Log.LogDebug($"{Name} set to {value}");

            RawValue = value;

            if (Setting != null && AmongUsClient.Instance.AmHost && SendRpc) Rpc.SendRpc(this);

            try
            {
                Setting.
                if (Setting is ToggleOption toggle && toggle.CheckMark != null)
                {
                    toggle.CheckMark.enabled = (bool)RawValue;
                }
                else if (Setting is NumberOption number)
                {
                    var newValue = (float)RawValue;

                    number.Value = number.oldValue = newValue;
                    number.ValueText.text = ToString();
                }
                else if (Setting is StringOption str)
                {
                    var newValue = (int)RawValue;

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