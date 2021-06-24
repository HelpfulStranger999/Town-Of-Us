using System;

namespace TownOfUs.PatchesArchive.CustomOption
{
    public class Range<T> where T : IComparable
    {
        public T MinimumValue { get; }
        public T MaximumValue { get; }

        public T Value { get; private set; }

        public Range(T min, T max, T initialValue)
        {
            MinimumValue = min;
            MaximumValue = max;
            Value = initialValue;
        }

        public T SetValue(T value)
        {
            if (value.CompareTo(MinimumValue) == -1) // value < MinimumValue
                Value = MinimumValue;
            else if (value.CompareTo(MaximumValue) == 1) // value > MaximumValue
                Value = MaximumValue;
            else
                Value = value;

            return Value;
        }
    }
}