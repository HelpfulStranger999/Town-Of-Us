using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TownOfUs.Extensions
{
    public static class AmongUsExtensions
    {
        public static KeyValuePair<byte, int> MaxPair(this Dictionary<byte, int> self, out bool tie)
        {
            tie = true;
            var result = new KeyValuePair<byte, int>(byte.MaxValue, int.MinValue);
            foreach (var keyValuePair in self)
                if (keyValuePair.Value > result.Value)
                {
                    result = keyValuePair;
                    tie = false;
                }
                else if (keyValuePair.Value == result.Value)
                {
                    tie = true;
                }

            return result;
        }

        public static KeyValuePair<byte, int> MaxPair(this byte[] self, out bool tie)
        {
            tie = true;
            var result = new KeyValuePair<byte, int>(byte.MaxValue, int.MinValue);
            for (byte i = 0; i < self.Length; i++)
                if (self[i] > result.Value)
                {
                    result = new KeyValuePair<byte, int>(i, self[i]);
                    tie = false;
                }
                else if (self[i] == result.Value)
                {
                    tie = true;
                }

            return result;
        }

        public static ReadOnlyCollection<PlayerVoteArea> GetAllVoteAreas(this MeetingHud instance)
        {
            var list = instance.playerStates.ToList();
            list.Add(instance.SkipVoteButton);
            return list.AsReadOnly();
        }

        public static void RenderUpdateOption(this OptionBehaviour behavior)
        { }

        private static void UpdateOption(this NumberOption numberOption, float value)
        {
            numberOption.Value = numberOption.oldValue = value;
            numberOption.ValueText.text = ToString();
        }

        private static void UpdateOption(this StringOption stringOption)
        {
            var newValue = (float)Value;

            number.Value = number.oldValue = newValue;
            number.ValueText.text = ToString();
        }

        private static void UpdateOption(this ToggleOption toggleOption, bool value)
        {
            toggleOption.oldValue = value;
            if (toggleOption.CheckMark != null)
            {
                toggleOption.CheckMark.enabled = value;
            }
        }
    }
}